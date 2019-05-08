using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.Extensions;
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
    public class SysRoleService : BaseServer<SysRole>, ISysRoleService
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
                await Db.Insertable(parm).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return res;
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
                res.data =await Db.Queryable<SysRole>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.DepartmentGroup.Contains(parm.key))
                        .OrderBy(m => m.AddTime).ToPageAsync(parm.page, parm.limit);
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<Page<SysRoleDto>>> GetPagesToRoleAsync(string key,string adminGuid)
        {
            var res = new ApiResult<Page<SysRoleDto>>();
            try
            {
                res.data =await Db.Queryable<SysRole>()
                        .WhereIF(!string.IsNullOrEmpty(key), m => m.DepartmentGroup.Contains(key))
                        .OrderBy(m => m.AddTime)
                        .Select(it => new SysRoleDto()
                        {
                            guid = it.Guid,
                            name = it.Name,
                            codes = it.Codes,
                            //status = SqlFunc.Subqueryable<SysPermissions>().Where(g => g.RoleGuid == it.Guid && g.AdminGuid == adminGuid && g.Types == 2).Any()
                        })
                        .Mapper((it, cache) => {
                            var list = cache.Get(g =>
                              {
                                  return Db.Queryable<SysPermissions>().Where(m=>m.AdminGuid==adminGuid && m.Types==2).ToList();
                              });
                            if (list.Any(m=>m.RoleGuid==it.guid))
                            {
                                it.status = true;
                            }
                            else
                            {
                                it.status = false;
                            }
                        })
                        .ToPageAsync(1, 100);
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
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

                var dbres =await Db.Updateable(parm).ExecuteCommandAsync();
                res.data= dbres>0 ? "1" : "0";
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }
    }
}
