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
    /// 调拨单服务接口实现
    /// </summary>
    public class ErpTransferService : DbContext, IErpTransferService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpTransfer parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                parm.Guid = Guid.NewGuid().ToString();
                var dbres = ErpTransferDb.Insert(parm);
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
                var dbres = ErpTransferDb.Update(m => new ErpTransfer() { IsDel = true }, m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpTransfer>> GetByGuidAsync(string parm)
        {
            var model = ErpTransferDb.GetById(parm);
            var res = new ApiResult<ErpTransfer>
            {
                statusCode = 200,
                data = model ?? new ErpTransfer() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<TransferDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<TransferDto>>();
            try
            {
                using (Db)
                {
                    var query = Db.Queryable<ErpTransfer>()
                        .Where(m => !m.IsDel)
                        .WhereIF(!string.IsNullOrEmpty(parm.key),m=>m.Number==parm.key)
                        .WhereIF(!string.IsNullOrEmpty(parm.guid),m=>m.InShopGuid==parm.guid || m.OutShopGuid==parm.guid)
                        .OrderBy(m => m.AddDate, OrderByType.Desc)
                        .Select(m=>new TransferDto() {
                            Guid=m.Guid,
                            Number=m.Number,
                            GoodsSum=m.GoodsSum,
                            AddDate=m.AddDate,
                            InShopName= SqlFunc.Subqueryable<ErpShops>().Where(g => g.Guid == m.InShopGuid).Select(g => g.ShopName),
                            OutShopName = SqlFunc.Subqueryable<ErpShops>().Where(g => g.Guid == m.OutShopGuid).Select(g => g.ShopName)
                        })
                        .ToPageAsync(parm.page, parm.limit);
                    res.success = true;
                    res.message = "获取成功！";
                    res.data = await query;
                }
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
        public async Task<ApiResult<string>> ModifyAsync(ErpTransfer parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpTransferDb.Update(parm);
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
