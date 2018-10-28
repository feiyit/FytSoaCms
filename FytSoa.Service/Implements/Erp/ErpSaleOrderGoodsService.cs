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
    public class ErpSaleOrderGoodsService : DbContext, IErpSaleOrderGoodsService
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(List<ErpSaleOrderGoods> parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                foreach (var item in parm)
                {
                    item.Guid = Guid.NewGuid().ToString();
                }
                var dbres = ErpSaleOrderGoodsDb.InsertRange(parm.ToArray());
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
        /// 根据订单编号查询多条记录
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<SaleOrderGoodsDto>>> GetPagesAsync(PageParm parm, SearchParm searchParm)
        {
            var res = new ApiResult<Page<SaleOrderGoodsDto>>();
            try
            {
                var query = Db.Queryable<ErpSaleOrderGoods, ErpGoodsSku>((eso, egs) => new object[] { JoinType.Left, eso.GoodsGuid == egs.Guid })
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), (eso, egs) => eso.OrderNumber == parm.guid)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.shopGuid), (eso, egs) => eso.ShopGuid == searchParm.shopGuid)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.brank), (eso, egs) => egs.BrankGuid == searchParm.brank)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.size), (eso, egs) => egs.SizeGuid == searchParm.size)
                    .Select((eso, egs) => new SaleOrderGoodsDto()
                    {
                        Guid=eso.Guid,
                        BrandName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.BrankGuid).Select(g => g.Name),
                        SeasonName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.SeasonGuid).Select(g => g.Name),
                        StyleName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.StyleGuid).Select(g => g.Name),
                        Code = egs.Code,
                        Counts = eso.Counts
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

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<SaleOrderGoodsDto>>> GetPagesListAsync(PageParm parm, SearchSaleOrderGoods searchParm)
        {
            var res = new ApiResult<Page<SaleOrderGoodsDto>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                var query = Db.Queryable<ErpSaleOrderGoods, ErpGoodsSku,ErpSaleOrder,ErpShops>((eso, egs,so,es) => 
                new object[] {
                    JoinType.Left, eso.GoodsGuid == egs.Guid,
                    JoinType.Left, eso.OrderNumber==so.Number,
                    JoinType.Left, eso.ShopGuid==es.Guid })
                    .WhereIF(!string.IsNullOrEmpty(parm.time), (eso, egs, so, es) => so.AddDate >= Convert.ToDateTime(beginTime) && so.AddDate <= Convert.ToDateTime(endTime))
                    .WhereIF(!string.IsNullOrEmpty(parm.key), (eso, egs, so, es) => eso.OrderNumber == parm.key)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.shopGuid), (eso, egs, so, es) => eso.ShopGuid == searchParm.shopGuid)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.brank), (eso, egs, so, es) => egs.BrankGuid == searchParm.brank)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.size), (eso, egs, so, es) => egs.SizeGuid == searchParm.size)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.year), (eso, egs, so, es) => egs.YearGuid == searchParm.year)
                    .WhereIF(!string.IsNullOrEmpty(searchParm.season), (eso, egs, so, es) => egs.SeasonGuid == searchParm.season)
                    .WhereIF(searchParm.backStatus!=-1, (eso, egs, so, es) => eso.BackCounts==searchParm.backStatus)
                    .WhereIF(searchParm.saleType != -1, (eso, egs, so, es) => so.SaleType == searchParm.saleType)
                    .Select((eso, egs, so, es) => new SaleOrderGoodsDto()
                    {
                        Guid = eso.Guid,
                        OrderNumber=eso.OrderNumber,
                        ShopName=es.ShopName,
                        ActivityName=so.ActivityName,
                        BrandName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.BrankGuid).Select(g => g.Name),
                        SeasonName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.SeasonGuid).Select(g => g.Name),
                        StyleName = SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.StyleGuid).Select(g => g.Name),
                        Code = egs.Code,
                        Money=eso.Money,
                        SaleType=so.SaleType,
                        BackCounts =eso.BackCounts,
                        Counts = eso.Counts,
                        AddDate = so.AddDate
                    })
                    .OrderByIF(!string.IsNullOrEmpty(parm.field) && !string.IsNullOrEmpty(parm.order), parm.field + " " + parm.order)
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
