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
using System.Linq;

namespace FytSoa.Service.Implements
{
    public class SysAdminService : BaseServer<SysAdmin>, ISysAdminService
    {
        #region  用户登录和授权菜单查询
        /// <summary>
        /// 用户登录实现
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysAdminMenuDto>> LoginAsync(SysAdminLogin parm)
        {
            var res = new ApiResult<SysAdminMenuDto>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var adminModel = new SysAdminMenuDto();
                parm.password = DES3Encrypt.EncryptString(parm.password);
                var model = Db.Queryable<SysAdmin>()
                        .Where(m => m.LoginName == parm.loginname).First();
                if (model==null)
                {
                    res.message = "账号错误";
                    return await Task.Run(() => res);
                }
                if (!model.LoginPwd.Equals(parm.password))
                {
                    res.message = "密码错误~";
                    return await Task.Run(() => res);
                }
                if (!model.Status)
                {
                    res.message = "登录账号被冻结，请联系管理员~";
                    return await Task.Run(() => res);
                }
                adminModel.menu = GetMenuByAdmin(model.Guid);
                if (adminModel==null)
                {
                    res.message = "当前账号没有授权功能模块，无法登录~";
                    return await Task.Run(() => res);
                }
                //修改登录时间
                model.LoginDate = DateTime.Now;
                model.UpLoginDate = model.LoginDate;
                SysAdminDb.Update(model);

                

                res.statusCode = (int)ApiEnum.Status;
                adminModel.admin = model;
                res.data = adminModel;
            }
            catch (Exception ex)
            {
                res.message =ex.Message;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return await Task.Run(() => res);
        }
        /// <summary>
        /// 根据登录账号，返回菜单信息
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        List<SysMenuDto> GetMenuByAdmin(string admin)
        {
            List<SysMenuDto> res = null;
            try
            {
                //根据用户查询角色列表， 一个用户对应多个角色
                var roleList = SysPermissionsDb.GetList(m => m.AdminGuid == admin && m.Types == 2).Select(m => m.RoleGuid).ToList();
                //根据角色查询菜单，并查询到菜单涉及的功能
                var query = Db.Queryable<SysMenu, SysPermissions>((sm, sp) => new object[]{
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
                        return Db.Queryable<SysCode>().Where(m => m.ParentGuid == "a88fa4d3-3658-4449-8f4a-7f438964d716")
                            .Select(m => new SysCodeDto()
                            {
                                guid = m.Guid,
                                name = m.Name,
                                codeType = m.CodeType
                            })
                            .ToList();
                    });
                    if (!string.IsNullOrEmpty(it.btnJson))
                    {
                        it.btnFun = codeList.Where(m => it.btnJson.Contains(m.guid)).ToList();
                    }
                });
                res = query.ToList();
            }
            catch
            {
                res = null;
            }
            return res;
        }
        #endregion

        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public new async Task<ApiResult<string>> AddAsync(SysAdmin parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = "1"
            };
            try
            {
                //判断用吗是否存在
                var isExisteName = SysAdminDb.IsAny(m => m.LoginName == parm.LoginName);
                if (isExisteName)
                {
                    res.message = "用户名已存在，请更换~";
                    res.statusCode = (int)ApiEnum.ParameterError;
                    return await Task.Run(() => res);
                }
                parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                if (string.IsNullOrEmpty(parm.HeadPic))
                {
                    parm.HeadPic = "/themes/img/avatar.jpg";
                }
                parm.Guid = Guid.NewGuid().ToString();
                parm.AddDate = DateTime.Now;
                if (!string.IsNullOrEmpty(parm.DepartmentGuid))
                {
                    // 说明有父级  根据父级，查询对应的模型
                    var model = SysOrganizeDb.GetById(parm.DepartmentGuid);
                    parm.DepartmentGuidList = model.ParentGuidList;
                }
                SysAdminDb.Insert(parm);
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }     
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public new async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var list = Utils.StrToListString(parm);
            var isok = SysAdminDb.Delete(m => list.Contains(m.Guid));
            //删除授权
            SysPermissionsDb.Delete(m=> list.Contains(m.MenuGuid) && m.Types==2);
            var res = new ApiResult<string>
            {
                statusCode = isok ? 200 : 500,
                data = isok ? "1" : "0",
                message = isok ? "删除成功~" : "删除失败~"
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 根据唯一编号查询一条部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysAdmin>> GetByGuidAsync(string parm)
        {
            var model = SysAdminDb.GetSingle(m => m.Guid == parm);
            var res = new ApiResult<SysAdmin>
            {
                statusCode = 200
            };
            res.data = model ?? new SysAdmin() {  };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public new async Task<ApiResult<Page<SysAdmin>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SysAdmin>>();
            try
            {
                var query = Db.Queryable<SysAdmin>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.DepartmentGuidList.Contains(parm.key))
                        .OrderBy(m => m.AddDate).ToPageAsync(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data = await query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(SysAdmin parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = 200                
            };
            try
            {
                //修改，判断用户是否和其它的重复
                var isExisteName = SysAdminDb.IsAny(m => m.LoginName == parm.LoginName && m.Guid!=parm.Guid);
                if (isExisteName)
                {
                    res.message = "用户名已存在，请更换~";
                    res.statusCode = (int)ApiEnum.ParameterError;
                    return await Task.Run(() => res);
                }

                parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                if (!string.IsNullOrEmpty(parm.DepartmentGuid))
                {
                    // 说明有父级  根据父级，查询对应的模型
                    var model = SysOrganizeDb.GetById(parm.DepartmentGuid);
                    parm.DepartmentGuidList = model.ParentGuidList;
                }
                var dbres = Db.Updateable<SysAdmin>().UpdateColumns(m => new SysAdmin()
                {
                    LoginName = parm.LoginName,
                    LoginPwd = parm.LoginPwd,
                    DepartmentName = parm.DepartmentName,
                    DepartmentGuid=parm.DepartmentGuid,
                    DepartmentGuidList=parm.DepartmentGuidList,
                    TrueName=parm.TrueName,
                    Number=parm.Number,
                    Sex=parm.Sex,
                    Mobile=parm.Mobile,
                    Email=parm.Email,
                    Status=parm.Status
                }).Where(m=>m.Guid==parm.Guid).ExecuteCommand();
                if (dbres>1)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "更新失败！";
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return await Task.Run(() => res);
        }
    }
}
