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

namespace FytSoa.Service.Implements
{
    public class ErpShopSkuService : DbContext, IErpShopSkuService
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(List<ErpShopSku> list)
        {
            var res = new ApiResult<string>();
            try
            {
                if (!list.Any())
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "数据库不能为空~";
                }
                var dbres = ErpShopSkuDb.InsertRange(list.ToArray());
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
        public Task<ApiResult<Page<ShopSkuDto>>> GetPagesAsync(PageParm parm, AppSearchParm searchParm)
        {
            var res = new ApiResult<Page<ShopSkuDto>>();
            try
            {
                var query = Db.Queryable<ErpShopSku, ErpShops, ErpGoodsSku>((t1, t2, t3) => new object[] {
                    JoinType.Left,t1.ShopGuid==t2.Guid,
                    JoinType.Left,t1.SkuGuid==t2.Guid
                })
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), (t1, t2, t3) => t1.ShopGuid == parm.guid)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.brand), (t1, t2, t3) => t3.BrankGuid == searchParm.brand)
                    .OrderByIF(parm.orderType == 1, (t1, t2, t3) =>t1.Sale,OrderByType.Desc)
                    .OrderBy((t1, t2, t3) => t1.Stock, OrderByType.Desc)
                    .Select((t1, t2, t3) =>new ShopSkuDto() {
                        Guid=t1.SkuGuid,
                        Code=t1.SkuCode,
                        BrankName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == t3.BrankGuid).Select(g => g.Name),
                        StyleName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == t3.StyleGuid).Select(g => g.Name),
                        SeasonName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == t3.SeasonGuid).Select(g => g.Name),
                        Stock =t1.Stock,
                        Sale=t1.Sale
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
