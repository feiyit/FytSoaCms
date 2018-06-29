using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
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
    /// 库存盘点服务实现
    /// </summary>
    public class InventoryService : DbContext, IInventoryService
    {
        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<StockInventory>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<StockInventory>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                var query = Db.Queryable<ErpGoodsSku>()
                    .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Code.Contains(parm.key))
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.BrankGuid == parm.guid)
                    .WhereIF(!string.IsNullOrEmpty(parm.time), m => m.AddDate >= Convert.ToDateTime(beginTime) && m.AddDate <= Convert.ToDateTime(endTime))
                    .PartitionBy(m => new { m.Code, m.Guid })
                    .Select(m => new StockInventory() {
                        Code = m.Code,
                        Sale = SqlFunc.AggregateSum(m.SaleSum),
                        Stock = SqlFunc.Subqueryable<ErpInOutLog>().Where(g => g.GoodsGuid == m.Guid && g.Types==1).Sum(g => g.GoodsSum),
                        //Stock = SqlFunc.Subqueryable<ErpInOutLog>().Where(g => g.GoodsGuid == m.Guid).Sum(g => g.GoodsSum),
                        Transfer = SqlFunc.Subqueryable<ErpTransferGoods>().Where(g => g.GoodsGuid == m.Guid).Sum(g => g.GoodsSum),
                        Return = SqlFunc.Subqueryable<ErpReturnGoods>().Where(g => g.GoodsGuid == m.Guid).Count(),
                        Back = SqlFunc.Subqueryable<ErpBackGoods>().Where(g => g.GoodsGuid == m.Guid).Count()
                    }).ToPage(parm.page, parm.limit);                
                res.success = true;
                res.message = "获取成功！";
                res.data = query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }
    }
}
