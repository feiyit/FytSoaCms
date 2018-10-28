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
    /// <summary>
    /// 条形码报损订单
    /// </summary>
    public class ErpSkuLossOrderService : DbContext, IErpSkuLossOrderService
    {
        /// <summary>
        /// 条件报损订单信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpSkuLossOrder parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.Error };
            try
            {
                var skuList = Utils.StrToListString(parm.SkuList);
                parm.Guid = Guid.NewGuid().ToString();
                var result = Db.Ado.UseTran(() =>
                {
                    Db.Insertable(parm).ExecuteCommand();
                    //更改报损信息为销售状态，并修改订单编号
                    Db.Updateable<ErpSkuLoss>()
                    .UpdateColumns(m => new ErpSkuLoss() {Types=1,OrderGuid=parm.Guid })
                    .Where(m=>skuList.Contains(m.Guid)).ExecuteCommand();
                });
                res.statusCode = (int)ApiEnum.Status;
                if (!result.IsSuccess)
                {
                    res.message = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() {statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpSkuLossOrderDb.Delete(m => list.Contains(m.Guid));
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
        /// 获得一条信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<ErpSkuLossOrder>> GetByGuidAsync(string parm)
        {
            var model = ErpSkuLossOrderDb.GetSingle(m=>m.Guid==parm);
            if (model==null)
            {
                var dayCount = ErpSkuLossOrderDb.Count(m => SqlFunc.DateIsSame(m.AddDate, DateTime.Now));
                model = new ErpSkuLossOrder() {
                    Number= "SL-" + DateTime.Now.ToString("yyyyMMdd") + "-" + (1001 + dayCount)
                };
            }
            var res = new ApiResult<ErpSkuLossOrder>
            {
                statusCode = 200,
                data = model ?? new ErpSkuLossOrder() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页查询报损订单信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpSkuLossOrder>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpSkuLossOrder>>();
            try
            {
                var query=Db.Queryable<ErpSkuLossOrder>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Number==parm.key || m.CustomerName.Contains(parm.key) || m.CustomerMobile==parm.key)
                        .OrderByIF(string.IsNullOrEmpty(parm.field) || string.IsNullOrEmpty(parm.order), m => m.AddDate, OrderByType.Desc)
                        .OrderByIF(!string.IsNullOrEmpty(parm.field) && !string.IsNullOrEmpty(parm.order), parm.field + " " + parm.order)
                        .ToPageAsync(parm.page, parm.limit);
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
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(ErpSkuLossOrder parm)
        {
            var res = new ApiResult<string>() { statusCode = 200 };
            try
            {
                var dbres = ErpSkuLossOrderDb.Update(parm);
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
