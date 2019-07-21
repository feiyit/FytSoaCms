using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Member;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Extensions;
using FytSoa.Service.Interfaces;
using SqlSugar;

namespace FytSoa.Service.Implements
{
    /*!
    * 文件名称：Bbs_user服务接口实现
    */
    public class MemberService : BaseServer<Member>, IMemberService
    {
        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Add(Member model)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                model.Guid = Guid.NewGuid().ToString();
                //判断账号是否存在
                var isexModel = Db.Queryable<Member>().Single(m => m.LoginName == model.LoginName);
                if (isexModel!=null)
                {
                    res.message = "用户名已存在，请更换~";
                    return res;
                }
                model.LoginPwd = DES3Encrypt.EncryptString(model.LoginPwd);
                await Db.Insertable(model).ExecuteCommandAsync();
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
        /// 修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Edit(Member model)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                //判断账号是否存在
                var isexModel = Db.Queryable<Member>().Single(m => m.LoginName == model.LoginName && m.Guid!=model.Guid);
                if (isexModel!=null)
                {
                    res.message = "用户名已存在，请更换~";
                    return res;
                }
                model.LoginPwd = DES3Encrypt.EncryptString(model.LoginPwd);
                await Db.Updateable(model).ExecuteCommandAsync();
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
        /// 查询用户信息-增加组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<Member>>> GetPageList(PageParm parm)
        {
            var res = new ApiResult<Page<Member>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data =await Db.Queryable<Member, Member_Group>((m,g)=>new object[] {
                    JoinType.Inner,m.Grade==g.Guid
                })
                    .Where((m, g) => !m.IsDel)
                    .OrderBy((m, g) => m.RegTime,OrderByType.Desc)
                    .Select<Member>()
                    .ToPageAsync(parm.page,parm.limit);
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<Member>> Login(List<FormParam> param)
        {
            var res = new ApiResult<Member>(){ statusCode = (int)ApiEnum.Error};
            try
            {
                if (param==null || param.Count==0)
                {
                    res.message = ApiEnum.ParameterError.GetEnumText();
                    return res;
                }

                var model =await Db.Queryable<Member>().SingleAsync(m => m.LoginName == param[0].value || m.Mobile==param[0].value || m.Email==param[0].value && !m.IsDel);
                if (model==null)
                {
                    res.message = "用户名输入错误~";
                    return res;
                }

                if (!model.Status)
                {
                    res.message = "账号被冻结，请联系客服专员~";
                    return res;
                }
                //密码处理
                var pass = DES3Encrypt.EncryptString(param[1].value);
                if (!pass.Equals(model.LoginPwd))
                {
                    res.message = "密码输入错误~";
                    return res;
                }

                //修改时间
                model.LoginSum += 1;
                model.LoginTime = DateTime.Now;
                await Db.Updateable(model).ExecuteCommandAsync();

                res.data = model;
                res.statusCode = (int) ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }

            return res;
        }
    }
}