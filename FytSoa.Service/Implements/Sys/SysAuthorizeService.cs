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
using Newtonsoft.Json;

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
        public ApiResult<List<SysMenuDto>> GetAuthorizeAsync(string admin)
        {
            var res = new ApiResult<List<SysMenuDto>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                //根据用户查询角色列表， 一个用户对应多个角色
                var roleList = SysPermissionsDb.GetList(m=>m.AdminGuid==admin && m.Types==2).Select(m=>m.RoleGuid).ToList();
                //根据角色查询菜单，并查询到菜单涉及的功能
                var query =Db.Queryable<SysMenu, SysPermissions>((sm, sp) => new object[]{
                    JoinType.Left,sm.Guid==sp.MenuGuid
                })
                .Where((sm, sp) => roleList.Contains(sp.RoleGuid) && sp.Types == 1 && sm.Status)
                .OrderBy((sm, sp) => sm.Sort)
                .Select((sm, sp) => new SysMenuDto()
                {
                    guid = sm.Guid,
                    parentGuid = sm.ParentGuid,
                    parentName = sm.ParentName,
                    name = sm.Name,
                    nameCode = sm.NameCode,
                    parentGuidList = sm.ParentGuidList,
                    layer = sm.Layer,
                    urls = sm.Urls,
                    icon = sm.Icon,
                    sort = sm.Sort,
                    btnJson = sp.BtnFunJson
                })
                .Mapper((it, cache) => {
                    var codeList = cache.Get(list =>
                      {
                          return Db.Queryable<SysCode>().Where(m=>m.ParentGuid== "a88fa4d3-3658-4449-8f4a-7f438964d716")
                          .Select(m=>new SysCodeDto() {
                              guid=m.Guid,
                              name=m.Name,
                              codeType=m.CodeType
                          })
                          .ToList();
                      });
                    if (!string.IsNullOrEmpty(it.btnJson))
                    {
                        it.btnFun = codeList.Where(m => it.btnJson.Contains(m.guid)).ToList();
                    }
                });
                res.data = query.ToList();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 根据菜单，获得当前菜单的所有功能权限
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<SysCodeDto>> GetCodeByMenu(string role, string menu)
        {
            var res = new ApiResult<List<SysCodeDto>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                //获得角色权限Guid-List
                var menuModel = SysMenuDb.GetSingle(m=>m.Guid==menu);
                //查询授权菜单里面的按钮功能
                var btnFunModel = SysPermissionsDb.GetSingle(m=>m.RoleGuid==role && m.MenuGuid==menu && m.Types==1);
                var codeList = new List<SysCodeDto>();
                if (!string.IsNullOrEmpty(menuModel.BtnFunJson))
                {
                    var list = JsonConvert.DeserializeObject<List<string>>(menuModel.BtnFunJson);
                    codeList =Db.Queryable<SysCode>().Where(m=>list.Contains(m.Guid)).Select(m=>new SysCodeDto() {
                        guid=m.Guid,
                        name=m.Name,
                        codeType=m.CodeType,
                        status=string.IsNullOrEmpty(btnFunModel.BtnFunJson)?false:btnFunModel.BtnFunJson.Contains(m.Guid)?true:false
                    }).ToList();
                }
                res.statusCode = (int)ApiEnum.Status;
                res.data = codeList;
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
