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
    public class SysRoleMenuService : DbContext, ISysRoleMenuService
    {
        /// <summary>
        /// 用户授权角色
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AdminToRoleAsync(SysRoleMenu parm,bool status)
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
                    var dbres=SysRoleMenuDb.Insert(new SysRoleMenu() {
                        RoleGuid=parm.RoleGuid,
                        MenuGuid=parm.MenuGuid,
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
                    SysRoleMenuDb.Delete(m => m.MenuGuid == parm.MenuGuid && m.RoleGuid == parm.RoleGuid && m.Types == 2);
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
        public async Task<ApiResult<List<SysRoleMenu>>> GetListAsync(string roleGuid)
        {
            var res = new ApiResult<List<SysRoleMenu>>
            {
                statusCode = 200
            };
            try
            {
                var query = Db.Queryable<SysRoleMenu>()
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
        public async Task<ApiResult<string>> SaveAsync(SysRoleMenu parm)
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
                Db.Deleteable<SysRoleMenu>().Where(m => m.RoleGuid == parm.RoleGuid && m.Types==1).ExecuteCommand();

                var list = new List<SysRoleMenu>();
                foreach (var item in Utils.SplitString(parm.MenuGuid,','))
                {
                    list.Add(new SysRoleMenu() {RoleGuid=parm.RoleGuid,MenuGuid=item,Types=parm.Types });
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
                Db.Ado.CommitTran();
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
    }
}
