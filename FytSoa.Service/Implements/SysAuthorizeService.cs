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
    /// 根据登录账号，获得相应权限服务实现
    /// </summary>
    public class SysAuthorizeService : DbContext, ISysAuthorizeService
    {
        /// <summary>
        /// 根据登录账号，获得相应权限
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenu>>> GetAuthorizeAsync(string admin)
        {
            var res = new ApiResult<List<SysMenu>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                //根据用户查询角色列表， 一个用户对应多个角色
                var roleList = SysPermissionsDb.GetList(m=>m.AdminGuid==admin && m.Types==2).Select(m=>m.RoleGuid).ToList();
                //根据角色获得多个菜单
                var menuList = SysPermissionsDb.GetList(m=>roleList.Contains(m.RoleGuid) && m.Types==1).Select(m=>m.MenuGuid).ToList();
                //根据权限菜单查询列表
                res.data = SysMenuDb.GetList(m=>menuList.Contains(m.Guid) && m.Status).OrderBy(m=>m.Sort).ToList();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
    }
}
