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
    public class SysAdminService : BaseServer<SysAdmin>, ISysAdminService
    {
        /// <summary>
        /// 用户登录实现
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysAdmin>> LoginAsync(SysAdminLogin parm)
        {
            var res = new ApiResult<SysAdmin>();
            try
            {
                parm.password = DES3Encrypt.EncryptString(parm.password);
                var model = Db.Queryable<SysAdmin>()
                        .Where(m => m.LoginName == parm.loginname).First();
                if (model != null)
                {
                    if (model.LoginPwd.Equals(parm.password))
                    {
                        //修改登录时间
                        model.LoginDate = DateTime.Now;
                        model.UpLoginDate = model.LoginDate;
                        SysAdminDb.Update(model);

                        #region 保存操作日志
                        var logModel = new SysLog() {
                            Guid=Guid.NewGuid().ToString(),
                            LoginName = model.LoginName,
                            DepartName = model.DepartmentName,
                            OptionTable = "SysAdmin",
                            Summary="登录操作",
                            IP=Utils.GetIp(),
                            LogType= (int)LogEnum.Login,
                            Urls= Utils.GetUrl(),
                            AddTime=DateTime.Now
                        };
                        SysLogDb.Insert(logModel);
                        #endregion

                        res.success = true;
                        res.message = "获取成功！";
                        res.data = model;
                    }
                    else
                    {
                        res.success = false;
                        res.statusCode = (int)ApiEnum.Error;
                        res.message = "密码错误~";
                    }
                }
                else
                {
                    res.success = false;
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "账号错误~";
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }


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
            }
            return await Task.Run(() => res);
        }
    }
}
