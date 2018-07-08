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
    public class ErpSaleOrderService : DbContext, IErpSaleOrderService
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpSaleOrder parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                parm.Guid = Guid.NewGuid().ToString();
                var dbres = ErpSaleOrderDb.Insert(parm);
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "插入数据失败~";
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
        /// 查询多条记录
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<SaleOrderDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SaleOrderDto>>();
            try
            {
                var query = Db.Queryable<ErpSaleOrder,ErpShops>((eso,es)=>new object[] { JoinType.Left,eso.ShopGuid==es.Guid})
                    .OrderBy((eso, es) => eso.AddDate, OrderByType.Desc)
                    .Select((eso, es) => new SaleOrderDto() {
                        Number= eso.Number,
                        ShopName=es.ShopName,
                        Types=SqlFunc.ToString(eso.Types),
                        Counts=eso.Counts,
                        ActivityName=eso.ActivityName,
                        Money=eso.Money,
                        AddDate=eso.AddDate
                    })
                    .ToPage(parm.page, parm.limit);
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
