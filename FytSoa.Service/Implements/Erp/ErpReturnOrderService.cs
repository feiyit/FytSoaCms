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
    public class ErpReturnOrderService : DbContext, IErpReturnOrderService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpReturnOrder parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpReturnOrderDb.Insert(parm);
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
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpReturnOrderDb.Update(m => new ErpReturnOrder() { IsDel = true }, m => list.Contains(m.Guid));
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
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ReturnOrderDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ReturnOrderDto>>();
            try
            {
                var query = Db.Queryable<ErpReturnOrder,ErpShops,ErpStaff>((ero,es,est)=>
                new object[] {JoinType.Left,ero.ShopGuid==es.Guid,JoinType.Left,es.Guid==est.ShopGuid })
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.ShopGuid == parm.guid)
                        .OrderBy((ero, es, est)=>ero.AddDate,OrderByType.Desc)
                        .Select((ero, es, est) => new ReturnOrderDto() {
                            Guid=ero.Guid,
                            Number= ero.Number,
                            ShopName=es.ShopName,
                            Operator=est.TrueName,
                            Mobile=est.Mobile,
                            Counts= ero.GoodsSum,
                            Status=SqlFunc.ToString(ero.Status),
                            AddDate= ero.AddDate
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
        
    }
}
