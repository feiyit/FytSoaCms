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
        /// 查询多条记录
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<SaleOrderGoodsDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SaleOrderGoodsDto>>();
            try
            {
                var query = Db.Queryable<ErpSaleOrderGoods, ErpGoodsSku>((eso, egs) => new object[] { JoinType.Left, eso.GoodsGuid == egs.Guid })
                    .Select((eso, egs) => new SaleOrderGoodsDto()
                    {
                        GoodsName= SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.BrankGuid).Select(g => g.Name) +
                            SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.SeasonGuid).Select(g => g.Name) +
                            SqlFunc.Subqueryable<SysCode>().Where(g => g.Guid == egs.StyleGuid).Select(g => g.Name),
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
    }
}
