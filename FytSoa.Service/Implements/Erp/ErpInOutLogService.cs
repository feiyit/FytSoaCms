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
    public class ErpInOutLogService : DbContext, IErpInOutLogService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpInOutLog parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.GetSkuArray(parm.GoodsSku);
                //查询品牌
                parm.Brank = SysCodeDb.GetSingle(m => m.ParentGuid == "7b664e3e-f58a-4e66-8c0f-be1458541d14" && m.CodeType == list[0])?.Name;
                //查询款式
                parm.Style= SysCodeDb.GetSingle(m => m.ParentGuid == "48458681-48b0-490a-a840-0ffcbe49f5d4" && m.CodeType == list[2])?.Name;
                parm.Guid = Guid.NewGuid().ToString();
                var dbres = ErpInOutLogDb.Insert(parm);
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
                var dbres = ErpInOutLogDb.Delete(m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpInOutLog>> GetByGuidAsync(string parm)
        {
            var model = ErpInOutLogDb.GetById(parm);
            var res = new ApiResult<ErpInOutLog>
            {
                statusCode = 200,
                data = model ?? new ErpInOutLog() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpInOutLog>>> GetPagesAsync(PageParm parm, SearchParm searchParm)
        {
            var res = new ApiResult<Page<ErpInOutLog>>();
            try
            {
                var query = Db.Queryable<ErpInOutLog>()
                        .WhereIF(parm.types != 0, m => m.Types == parm.types)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.GoodsSku == parm.key)
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.ShopGuid == parm.guid)
                        .WhereIF(!string.IsNullOrEmpty(searchParm.brank), m => m.Brank == searchParm.brank)
                        .Where(m => m.PackGuid == searchParm.packGuid)
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

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(ErpInOutLog parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.GetSkuArray(parm.GoodsSku);
                //查询品牌
                parm.Brank = SysCodeDb.GetSingle(m => m.ParentGuid == "7b664e3e-f58a-4e66-8c0f-be1458541d14" && m.CodeType == list[0])?.Name;
                //查询款式
                parm.Style = SysCodeDb.GetSingle(m => m.ParentGuid == "48458681-48b0-490a-a840-0ffcbe49f5d4" && m.CodeType == list[2])?.Name;
                var dbres = ErpInOutLogDb.Update(parm);
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
