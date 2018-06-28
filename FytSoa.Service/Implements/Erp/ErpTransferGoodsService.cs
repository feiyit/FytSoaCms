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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 调拨单商品服务接口实现
    /// </summary>
    public class ErpTransferGoodsService : DbContext, IErpTransferGoodsService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpTransferGoods parm, List<TransferGoods> list)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //根据调拨单，查询入库信息
                var transferModel = ErpTransferDb.GetById(parm.TransferGuid);
                if (transferModel==null)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "调拨单不存在~";
                    return await Task.Run(() => res);
                }
                //增加调拨商品日志
                var listModel = new List<ErpTransferGoods>();
                //增加调拨的入库
                var inStockList = new List<ErpInOutLog>();
                //更改调拨出库的库存
                var skuArray = list.Select(m=>m.guid).ToList();
                var outStockList = ErpInOutLogDb.GetList(m=>m.ShopGuid==transferModel.OutShopGuid 
                && skuArray.Contains(m.GoodsGuid));
                //循环操作
                foreach (var item in list)
                {
                    //出库减少库存
                    var sourceStockSum=outStockList.Find(m=>m.GoodsGuid==item.guid).GoodsSum;
                    outStockList.Find(m => m.GoodsGuid == item.guid).GoodsSum = sourceStockSum - item.goodsSum;

                    //入库
                    inStockList.Add(new ErpInOutLog() {
                        Guid = Guid.NewGuid().ToString(),
                        Types=2,
                        ShopGuid= transferModel.InShopGuid,
                        PackGuid = "Transfer",
                        GoodsGuid=item.guid,
                        GoodsSum=item.goodsSum,
                        AddDate=DateTime.Now,
                        AdminGuid= parm.GoodsGuid,
                        InTypes=2
                    });

                    //调拨单商品
                    listModel.Add(new ErpTransferGoods() {
                        Guid= Guid.NewGuid().ToString(),
                        TransferGuid=parm.TransferGuid,
                        GoodsGuid=item.guid,
                        GoodsSum=item.goodsSum
                    });
                }
                //判断商品总数是否大于调拨单商品数量
                var transgerModel = ErpTransferDb.GetById(parm.TransferGuid);
                if (transgerModel.GoodsSum<listModel.Sum(m=>m.GoodsSum))
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "调拨数量不能大于调拨单总数~";
                    return await Task.Run(() => res);
                }

                //开启事务
                Db.Ado.BeginTran();
                //插入调拨入库信息
                ErpInOutLogDb.InsertRange(inStockList.ToArray());
                //更新调拨出库库存
                ErpInOutLogDb.UpdateRange(outStockList.ToArray());
                var dbres = ErpTransferGoodsDb.InsertRange(listModel.ToArray());
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "插入数据失败~";
                }
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.CommitTran();
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
                var dbres = ErpTransferGoodsDb.Delete(m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpTransferGoods>> GetByGuidAsync(string parm)
        {
            var model = ErpTransferGoodsDb.GetById(parm);
            var res = new ApiResult<ErpTransferGoods>
            {
                statusCode = 200,
                data = model ?? new ErpTransferGoods() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<TransferGoodsDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<TransferGoodsDto>>();
            try
            {
                var query = Db.Queryable<ErpTransferGoods>()
                        .Where(m => m.TransferGuid == parm.guid)
                        .Select(m=>new TransferGoodsDto() {
                            Guid=m.Guid,
                            GoodsSum=m.GoodsSum,
                            Code= SqlFunc.Subqueryable<ErpGoodsSku>().Where(g => g.Guid == m.GoodsGuid).Select(g => g.Code),
                            BrankName= SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == SqlFunc.Subqueryable<ErpGoodsSku>().Where(f => f.Guid == m.GoodsGuid).Select(f => f.BrankGuid)).Select(g => g.Name),
                            StyleName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == SqlFunc.Subqueryable<ErpGoodsSku>().Where(f => f.Guid == m.GoodsGuid).Select(f => f.StyleGuid)).Select(g => g.Name)
                        })
                        .ToPageAsync(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data = await query;
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
        public async Task<ApiResult<string>> ModifyAsync(ErpTransferGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpTransferGoodsDb.Update(parm);
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
