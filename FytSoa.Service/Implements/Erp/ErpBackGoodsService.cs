using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    public class ErpBackGoodsService : DbContext, IErpBackGoodsService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpBackGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                DateTime dayTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString() + " 00:00:00");
                parm.Guid = Guid.NewGuid().ToString();
                //查询今天退货数量
                var dayCount = ErpBackGoodsDb.Count(m => SqlFunc.DateIsSame(m.AddDate, dayTime));
                parm.Number = "BO-" + DateTime.Now.ToString("yyyyMMdd") + "-" + (1001 + dayCount);
                //根据条形码查询唯一编号
                var goodSku = ErpGoodsSkuDb.GetSingle(m=>m.Code==parm.GoodsGuid);
                if (goodSku!=null)
                {
                    parm.GoodsGuid = goodSku.Guid;
                }
                else
                {
                    res.message = "商品不存在~";
                    return await Task.Run(() => res);
                }
                //判断退货商品，金额是否大于订单金额，   以及商品数量，是否大于订单出售数量
                var orderModel = ErpSaleOrderDb.GetSingle(m=>m.Number==parm.OrderNumber);
                if (orderModel==null)
                {
                    res.message = "订单不存在~";
                    return await Task.Run(() => res);
                }
                if (parm.BackMoney>orderModel.RealMoney)
                {
                    res.message = "退货金额不能大于订单金额~";
                    return await Task.Run(() => res);
                }
                //判断是否存在
                var isExt = ErpBackGoodsDb.IsAny(m => m.ShopGuid == parm.ShopGuid && m.GoodsGuid==parm.GoodsGuid && m.OrderNumber==parm.OrderNumber);
                if (isExt)
                {
                    res.message = "该退货信息已存在~";
                    return await Task.Run(() => res);
                }
                var result = Db.Ado.UseTran(() =>
                {
                    //修改加盟商条形码里面的库存 退货=加盟商库存增加
                    Db.Updateable<ErpShopSku>()
                    .UpdateColumns(m=>new ErpShopSku() { Stock=m.Stock+parm.BackCount })
                    .Where(m=>m.ShopGuid==parm.ShopGuid && m.SkuGuid==parm.GoodsGuid)
                    .ExecuteCommand();
                    //增加一条退货信息
                    Db.Insertable(parm).ExecuteCommand();
                });
                res.statusCode = (int)ApiEnum.Status;
                if (!result.IsSuccess)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpBackGoodsDb.Delete(m => list.Contains(m.Guid));
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "删除数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ErpBackGoods>> GetByGuidAsync(string parm)
        {
            var model = ErpBackGoodsDb.GetById(parm);
            var res = new ApiResult<ErpBackGoods>
            {
                statusCode = 200,
                data = model ?? new ErpBackGoods() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<BackGoodsDto>>> GetPagesAsync(PageParm parm, SearchParm searchParm)
        {
            var res = new ApiResult<Page<BackGoodsDto>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                var query = Db.Queryable<ErpBackGoods,ErpGoodsSku,ErpShops,ErpStaff>((ebg,eg,es,est)=>new object[] {
                    JoinType.Left,ebg.GoodsGuid==eg.Guid,
                    JoinType.Left,ebg.ShopGuid==es.Guid,
                    JoinType.Left,ebg.AdminGuid==est.Guid})
                        .WhereIF(!string.IsNullOrEmpty(searchParm.shopGuid), (ebg, eg, es, est) => ebg.ShopGuid == searchParm.shopGuid)
                        .WhereIF(!string.IsNullOrEmpty(searchParm.brank), (ebg, eg, es, est) => eg.BrankGuid == searchParm.brank)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), (ebg, eg, es, est) => ebg.Number == parm.key)
                        .WhereIF(!string.IsNullOrEmpty(parm.time), (ebg, eg, es, est) => ebg.AddDate >= Convert.ToDateTime(beginTime) && ebg.AddDate <= Convert.ToDateTime(endTime))
                        .Select((ebg, eg, es, est) => new BackGoodsDto() {
                            Code=eg.Code,
                            OrderNumber=ebg.OrderNumber,
                            BrandName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.BrankGuid).Select(g => g.Name),
                            SeasonName=SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.SeasonGuid).Select(g => g.Name),
                            StyleName=SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == eg.StyleGuid).Select(g => g.Name),
                            ShopName =es.ShopName,
                            Operator=est.TrueName,
                            Mobile=est.Mobile,
                            BackCount =ebg.BackCount,
                            Money=ebg.BackMoney,
                            Summary=ebg.Summary,
                            AddDate =ebg.AddDate
                        })
                        .ToPage(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data =  query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(ErpBackGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpBackGoodsDb.Update(parm);
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "修改数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
    }
}
