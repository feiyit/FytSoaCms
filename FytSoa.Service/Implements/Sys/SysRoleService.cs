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
    public class SysRoleService : BaseService<SysRole>, ISysRoleService
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
                parm.Guid = Guid.NewGuid().ToString();
                parm.EditTime = DateTime.Now;
                parm.AddTime = DateTime.Now;
                if (parm.Level==1)
                {
                    //根据部门ID查询部门
                    var organizeModel = SysOrganizeDb.GetById(parm.DepartmentGuid);
                    parm.DepartmentGroup = organizeModel.ParentGuidList;
                }
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
                var list=await Db.Queryable<SysRole>()
                        //.WhereIF(!string.IsNullOrEmpty(parm.key), m => m.DepartmentGroup.Contains(parm.key))
                        .OrderBy(m => m.Sort,OrderByType.Desc)
                        .OrderBy(m => m.AddTime, OrderByType.Desc)
                        .ToPageAsync(parm.page, parm.limit);

                var tree = list.Items;
                var newList = new List<SysRole>();
                foreach (var item in tree.Where(m=>m.Level==0).ToList())
                {
                    //查询角色
                    var tempRole = tree.Where(m => m.ParentGuid == item.Guid && m.Level == 1).ToList();
                    if (!string.IsNullOrEmpty(parm.key))
                    {
                        tempRole = tempRole.Where(m=>m.DepartmentGroup.Contains(parm.key)).ToList();
                    }
                    //if (tempRole.Count>0)
                    //{
                        newList.Add(item);
                    //}
                    foreach (var row in tempRole)
                    {
                        row.Name = "　|--" + row.Name;
                        newList.Add(row);
                    }
                }
                //赋值新的数组
                list.Items = newList;
                res.data = list;
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
                var reslist =await Db.Queryable<SysRole>()
                        //.WhereIF(!string.IsNullOrEmpty(key), m => m.DepartmentGroup.Contains(key))
                        .OrderBy(m => m.Sort, OrderByType.Desc)
                        .OrderBy(m => m.AddTime, OrderByType.Desc)
                        .Select(it => new SysRoleDto()
                        {
                            guid = it.Guid,
                            name = it.Name,
                            DepartmentGroup=it.DepartmentGroup,
                            ParentGuid=it.ParentGuid,
                            sort=it.Sort,
                            level=it.Level,
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
                        .ToPageAsync(1, 10000);

                var tree = reslist.Items;
                var newList = new List<SysRoleDto>();
                foreach (var item in tree.Where(m => m.level == 0).ToList())
                {
                    //查询角色
                    var tempRole = tree.Where(m => m.ParentGuid == item.guid && m.level == 1).ToList();
                    if (!string.IsNullOrEmpty(key))
                    {
                        tempRole = tempRole.Where(m => m.DepartmentGroup.Contains(key)).ToList();
                    }
                    if (tempRole.Count > 0)
                    {
                        newList.Add(item);
                    }
                    foreach (var row in tempRole)
                    {
                        row.name = "　|--" + row.name;
                        newList.Add(row);
                    }
                }
                //赋值新的数组
                reslist.Items = newList;
                res.data = reslist;
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
            var res = new ApiResult<string>() { statusCode = 200 };
            try
            {
                parm.EditTime = DateTime.Now;
                if (parm.Level==1)
                {
                    //根据部门ID查询部门组
                    var organizeModel = SysOrganizeDb.GetById(parm.DepartmentGuid);
                    parm.DepartmentGroup = organizeModel.ParentGuidList;
                }
                await Db.Updateable(parm).ExecuteCommandAsync();
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
