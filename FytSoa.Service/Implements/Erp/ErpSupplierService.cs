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
    /// 供应商接口服务实现
    /// </summary>
    public class ErpSupplierService : DbContext, IErpSupplierService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpSupplier parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断是否存在
                var isExt = ErpSupplierDb.IsAny(m => m.Name == parm.Name);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该信息已存在~";
                }
                else
                {
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = ErpSupplierDb.Insert(parm);
                    if (!dbres)
                    {
                        res.statusCode = (int)ApiEnum.Error;
                        res.message = "插入数据失败~";
                    }
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
                var dbres = ErpSupplierDb.Update(m => new ErpSupplier() { IsDel = true }, m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpSupplier>> GetByGuidAsync(string parm)
        {
            var model = ErpSupplierDb.GetById(parm);
            var res = new ApiResult<ErpSupplier>
            {
                statusCode = 200,
                data = model ?? new ErpSupplier() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpSupplier>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpSupplier>>();
            try
            {
                using (Db)
                {
                    var query = Db.Queryable<ErpSupplier>()
                        .Where(m => !m.IsDel)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Name.Contains(parm.key) || m.Mobile == parm.key || m.Contacts.Contains(parm.key))
                        .OrderBy(m => m.AddDate, OrderByType.Desc)
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
        public async Task<ApiResult<string>> ModifyAsync(ErpSupplier parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpSupplierDb.Update(parm);
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
