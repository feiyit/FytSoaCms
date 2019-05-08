using FytSoa.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 角色关联菜单的实现
    /// </summary>
    public class SysPermissionsService : DbContext, ISysPermissionsService
    {
        /// <summary>
        /// 用户授权角色
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ToRoleAsync(SysPermissions parm,bool status)
        {
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = "1"
            };
            try
            {
                if (status)
                {
                    //授权
                    var dbres =await Db.Insertable<SysPermissions>(new SysPermissions()
                    {
                        RoleGuid = parm.RoleGuid,
                        AdminGuid = parm.AdminGuid,
                        MenuGuid = parm.MenuGuid,
                        BtnFunJson = parm.BtnFunJson,
                        Types = parm.Types
                    }).ExecuteCommandAsync();
                    
                    if (dbres==0)
                    {
                        res.statusCode = (int)ApiEnum.Error;
                        res.message = "插入数据失败~";
                    }
                }
                else
                {
                    //取消授权
                    if (parm.Types==2)
                    {
                        await Db.Deleteable<SysPermissions>().Where(m => m.AdminGuid == parm.AdminGuid && m.RoleGuid == parm.RoleGuid && m.Types == 2).ExecuteCommandAsync();
                    }
                    if (parm.Types==3)
                    {
                        //角色-菜单-按钮功能
                        await Db.Deleteable<SysPermissions>().Where(m => m.BtnFunJson == parm.BtnFunJson && m.RoleGuid == parm.RoleGuid && m.MenuGuid == parm.MenuGuid && m.Types == 3).ExecuteCommandAsync();
                    }
                }                
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 根据角色ID查询授权的菜单
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<SysPermissions>>> GetListAsync(string roleGuid)
        {
            var res = new ApiResult<List<SysPermissions>>
            {
                statusCode = 200
            };
            try
            {
                res.data =await Db.Queryable<SysPermissions>()
                        .WhereIF(!string.IsNullOrEmpty(roleGuid), m => m.RoleGuid==roleGuid).ToListAsync();          
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 保存授权菜单
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SaveAsync(SysPermissions parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = "1"
            };
            try
            {
                //根据角色查询菜单列表
                var menuList =await Db.Queryable<SysPermissions>().Where(m=>m.RoleGuid==parm.RoleGuid && m.Types==1).ToListAsync();
                //现有的
                var list = new List<SysPermissions>();
                foreach (var item in Utils.SplitString(parm.MenuGuid, ','))
                {
                    list.Add(new SysPermissions() { RoleGuid = parm.RoleGuid, MenuGuid = item, Types = parm.Types });
                }
                //查询都有的
                var publicMenu = menuList.Where(m=>list.Exists(t=>t.MenuGuid==m.MenuGuid)).ToList();

                //查询数据库有，但是前端提供没有的，说明要删除这个菜单权限
                var delMenuList = menuList.Where(m=>!publicMenu.Exists(t=>t.MenuGuid==m.MenuGuid)).Select(m=>m.MenuGuid).ToList();

                //查询前端提供的，数据库没有的，说明概要增加菜单权限
                var AddMenuList = list.Where(m => !publicMenu.Exists(t => t.MenuGuid == m.MenuGuid)).ToList();

                var result = Db.Ado.UseTran(async () =>
                {
                    //删除差异的
                    if (delMenuList.Count>0)
                    {
                        await Db.Deleteable<SysPermissions>().Where(m => m.RoleGuid == parm.RoleGuid && delMenuList.Contains(m.MenuGuid) && m.Types == 1).ExecuteCommandAsync();
                    }
                    //添加新的授权菜单
                    if (AddMenuList.Count>0)
                    {
                        await Db.Insertable(AddMenuList).ExecuteCommandAsync();
                    }
                });
                if (!result.IsSuccess)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "插入数据失败~";
                }                
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 菜单授权-菜单功能
        /// role=角色
        /// funguid=按钮的编号
        /// status=取消还是授权
        /// </summary>
        /// <returns></returns>
        public ApiResult<string> RoleMenuToFunAsync(SysPermissionsParm parm)
        {
            var res = new ApiResult<string>() { statusCode=(int)ApiEnum.Error};
            try
            {
                //根据角色和菜单查询内容
                var model = Db.Queryable<SysPermissions>().Single(m=>m.RoleGuid==parm.role
                && m.MenuGuid==parm.menu && m.Types==1);
                if (model==null)
                {
                    res.message = "您还没有授权当前菜单功能模块";
                    return res;
                }
                if (!string.IsNullOrEmpty(model.BtnFunJson))
                {
                    //判断授权还是取消
                    var list = JsonConvert.DeserializeObject<List<string>>(model.BtnFunJson);
                    if (parm.status==0)
                    {
                        if (list.Contains(parm.btnfun))
                        {
                            list.Remove(parm.btnfun);
                        }
                    }
                    else
                    {
                        //授权
                        if (!list.Contains(parm.btnfun))
                        {
                            list.Add(parm.btnfun);
                        }
                    }
                    model.BtnFunJson = JsonConvert.SerializeObject(list);
                }
                else
                {
                    //肯定是增加
                    model.BtnFunJson = JsonConvert.SerializeObject(new List<string>() { parm.btnfun });
                }
                Db.Updateable<SysPermissions>()
                    .UpdateColumns(m=>new SysPermissions() { BtnFunJson=model.BtnFunJson })
                    .Where(m => m.RoleGuid == parm.role
                && m.MenuGuid == parm.menu && m.Types == 1).ExecuteCommand();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 权限管理-保存角色和授权菜单以及功能
        /// </summary>
        /// <returns></returns>
        public ApiResult<string> SaveAuthorization(List<SysMenuDto> list, string roleGuid)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                if (list.Count == 0 && string.IsNullOrEmpty(roleGuid))
                {
                    res.message = ApiEnum.ParameterError.GetEnumText();
                    return res;
                }
                //查询有有数据的菜单
                var newList = new List<SysMenuDto>();
                foreach (var item in list)
                {
                    if (item.isChecked)
                    {
                        newList.Add(item);
                    }
                }
                if (newList.Count == 0)
                {
                    res.message = ApiEnum.ParameterError.GetEnumText();
                    return res;
                }
                //构建新的保存数组
                var dbList = new List<SysPermissions>();
                foreach (var item in newList)
                {
                    var btnFun = "";
                    if (item.btnFun != null && item.btnFun.Count > 0)
                    {
                        //查询授权status=true
                        var btnList = item.btnFun.Where(m=>m.status).Select(m=>m.guid).ToList();
                        if (btnList.Count>0)
                        {
                            btnFun = JsonConvert.SerializeObject(btnList);
                        }
                    }
                    dbList.Add(new SysPermissions()
                    {
                        RoleGuid = roleGuid,
                        MenuGuid = item.guid,
                        BtnFunJson = btnFun,
                        Types = 1
                    });
                }
                
                var result = Db.Ado.UseTran(async () =>
                {
                    //根据角色删除已有的，添加新的
                    await Db.Deleteable<SysPermissions>().Where(m=>m.RoleGuid==roleGuid && m.Types==1).ExecuteCommandAsync();
                    //增加新的
                    await Db.Insertable(dbList).ExecuteCommandAsync();
                });
                if (!result.IsSuccess)
                {
                    res.message = result.ErrorMessage;
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }
    }
}
