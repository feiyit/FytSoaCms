using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 采购单接口服务实现
    /// </summary>
    public class ErpPurchaseService : DbContext, IErpPurchaseService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpPurchase parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //查询今天又多少条采购单，并生成订单号
                var dayCount = ErpPurchaseDb.Count(m=> SqlFunc.DateIsSame(m.AddDate, DateTime.Now));
                parm.Guid = Guid.NewGuid().ToString();
                parm.Number = Utils.PurchaseNumber(1001+ dayCount);

                //分析商品并保存
                var list = new List<ErpPurchaseGoods>();
                if (!string.IsNullOrEmpty(parm.GoodsList))
                {
                    list = JsonConvert.DeserializeObject<List<ErpPurchaseGoods>> (parm.GoodsList).Where(m=>!string.IsNullOrEmpty(m.Number) && !string.IsNullOrEmpty(m.Name)).ToList();
                    var jsonCount = list.Count;
                    for (int i = 0; i < jsonCount; i++)
                    {
                        var item = list[i];
                        item.Guid = Guid.NewGuid().ToString();
                        item.PurchaseGuid = parm.Guid;
                        parm.Money += item.Quantity * item.Price;
                    }
                }
                Db.Ado.BeginTran();
                Db.Insertable(list).ExecuteCommand();
                Db.Insertable(parm).ExecuteCommand();               
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
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
                var dbres = ErpPurchaseDb.Update(m => new ErpPurchase() { IsDel = true }, m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpPurchase>> GetByGuidAsync(string parm)
        {
            var model = ErpPurchaseDb.GetById(parm);
            var res = new ApiResult<ErpPurchase>
            {
                statusCode = 200,
                data = model ?? new ErpPurchase() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<PurchaseDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<PurchaseDto>>();
            try
            {
                var query = Db.Queryable<ErpPurchase>()
                        .Where(m => !m.IsDel)
                        .WhereIF(parm.types != 0, m => m.Status == parm.types)
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.SupplierGuid == parm.guid)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Mobile == parm.key || m.Contacts.Contains(parm.key))
                        .OrderBy(m => m.AddDate, OrderByType.Desc)
                        .Select(m=>new PurchaseDto() {
                            Guid=m.Guid,
                            Number=m.Number,
                            Supplier = SqlFunc.Subqueryable<ErpSupplier>().Where(g => g.Guid == m.SupplierGuid).Select(g => g.Name),
                            AddDate=m.AddDate,
                            Money=m.Money,
                            DeliverDate=m.DeliverDate,
                            DeliverCity=m.DeliverCity,
                            Status= SqlFunc.ToString(m.Status)
                        })
                        .ToPageAsync(parm.page, parm.limit);
                foreach (var item in query.Result?.Items)
                {
                    item.Status = Utils.PurchaseStatus(item.Status);
                }
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
        public async Task<ApiResult<string>> ModifyAsync(ErpPurchase parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //分析商品并保存
                var list = new List<ErpPurchaseGoods>();
                if (!string.IsNullOrEmpty(parm.GoodsList))
                {
                    list = JsonConvert.DeserializeObject<List<ErpPurchaseGoods>>(parm.GoodsList);
                    for (int i = 0; i < list.Count; i++)
                    {
                        var item = list[i];
                        if (string.IsNullOrEmpty(item.Number) && string.IsNullOrEmpty(item.Name))
                        {
                            list.Remove(item);
                        }
                        else
                        {
                            item.Guid = Guid.NewGuid().ToString();
                            item.PurchaseGuid = parm.Guid;
                            parm.Money += item.Quantity * item.Price;
                        }
                    }
                }
                Db.Ado.BeginTran();
                //先删除以保存的商品
                Db.Deleteable<ErpPurchaseGoods>().Where(m=>m.PurchaseGuid==parm.Guid).ExecuteCommand();
                //保存新的商品
                Db.Insertable(list).ExecuteCommand();
                //修改新的采购单
                Db.Updateable(parm).ExecuteCommand();
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改采购单状态
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyStatusAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpPurchaseDb.Update(m => new ErpPurchase() { Status = (m.Status + 1) }, m => list.Contains(m.Guid));
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "变更数据失败~";
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
