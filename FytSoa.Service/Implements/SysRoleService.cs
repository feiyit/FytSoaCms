using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 角色功能实现
    /// </summary>
    public class SysRoleService : DbContext, ISysRoleService
    {
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(SysRole parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //根据部门ID查询部门组
                var organizeModel = SysOrganizeDb.GetById(parm.DepartmentGuid);
                parm.DepartmentGroup = organizeModel.ParentGuidList;

                parm.Guid = Guid.NewGuid().ToString();
                parm.EditTime = DateTime.Now;
                parm.AddTime = DateTime.Now;
                parm.IsSystem = true;
                SysRoleDb.Insert(parm);                
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var isok = SysRoleDb.Delete(m => list.Contains(m.Guid));
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 根据唯一编号查询一条部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysRole>> GetByGuidAsync(string parm)
        {
            var model = SysRoleDb.GetSingle(m => m.Guid == parm);
            var res = new ApiResult<SysRole>
            {
                statusCode = 200,
                data = model ?? new SysRole() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<Page<SysRole>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SysRole>>();
            try
            {
                var query = Db.Queryable<SysRole>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.DepartmentGroup.Contains(parm.key))
                        .OrderBy(m => m.AddTime).ToPageAsync(parm.page, parm.limit);
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
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<SysRoleDto>>> GetPagesToRoleAsync(string key,string adminGuid)
        {
            var res = new ApiResult<Page<SysRoleDto>>();
            try
            {
                var query = Db.Queryable<SysRole>()
                        .WhereIF(!string.IsNullOrEmpty(key), m => m.DepartmentGroup.Contains(key))
                        .OrderBy(m => m.AddTime)
                        .Select(it => new SysRoleDto()
                        {
                            guid = it.Guid,
                            name = it.Name,
                            codes = it.Codes,
                            status = SqlFunc.Subqueryable<SysPermissions>().Where(g => g.RoleGuid == it.Guid && g.AdminGuid == adminGuid && g.Types == 2).Any()
                        })
                        .ToPage(1, 100);
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
        /// 修改菜单
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(SysRole parm)
        {                    
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //根据部门ID查询部门组
                var organizeModel = SysOrganizeDb.GetById(parm.DepartmentGuid);
                parm.DepartmentGroup = organizeModel.ParentGuidList;

                parm.IsSystem = true;
                parm.EditTime = DateTime.Now;
                res.data=SysRoleDb.Update(parm) ? "1" : "0";
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
