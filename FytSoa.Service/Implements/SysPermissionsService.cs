using FytIms.Service.Extensions;
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
                    var dbres= SysPermissionsDb.Insert(new SysPermissions() {
                        RoleGuid=parm.RoleGuid,
                        AdminGuid=parm.AdminGuid,
                        MenuGuid=parm.MenuGuid,
                        BtnFunGuid=parm.BtnFunGuid,
                        Types=parm.Types
                    });
                    if (!dbres)
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
                        SysPermissionsDb.Delete(m => m.AdminGuid == parm.AdminGuid && m.RoleGuid == parm.RoleGuid && m.Types == 2);
                    }
                    if (parm.Types==3)
                    {
                        //角色-菜单-按钮功能
                        SysPermissionsDb.Delete(m => m.BtnFunGuid == parm.BtnFunGuid && m.RoleGuid == parm.RoleGuid && m.MenuGuid==parm.MenuGuid && m.Types == 3);
                    }
                }                
            }
            catch (Exception ex)
            {
                Db.Ado.CommitTran();
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
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
                var query = Db.Queryable<SysPermissions>()
                        .WhereIF(!string.IsNullOrEmpty(roleGuid), m => m.RoleGuid==roleGuid).ToListAsync();          
                res.data = await query;
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
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
                //开启事务
                Db.Ado.BeginTran();
                //先根据角色判断是否存在，如果存在，则删除
                Db.Deleteable<SysPermissions>().Where(m => m.RoleGuid == parm.RoleGuid && m.Types==1).ExecuteCommand();

                var list = new List<SysPermissions>();
                foreach (var item in Utils.SplitString(parm.MenuGuid,','))
                {
                    list.Add(new SysPermissions() {RoleGuid=parm.RoleGuid,MenuGuid=item,Types=parm.Types });
                }
                var dbres=Db.Insertable(list).ExecuteCommand();
                if (dbres==0)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "插入数据失败~";
                }
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
    }
}
