using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    public class SysAdminService : DbContext, ISysAdminService
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
    }
}
