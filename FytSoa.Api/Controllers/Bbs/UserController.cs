using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FytSoa.Api.Controllers.Bbs
{
    /// <summary>
    /// 前台使用
    /// </summary>
    [Route("api/bbs/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IBbs_QuestionsService _questionService;
        private readonly IBbs_AnswerService _answerService;
        public UserController(IMemberService memberService,
            IBbs_QuestionsService questionService
            , IBbs_AnswerService answerService)
        {
            _memberService = memberService;
            _questionService = questionService;
            _answerService = answerService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Add([FromBody]List<FormParam> param)
        {
            var res = await _memberService.Login(param);
            if (res.statusCode != 200) return Ok(res);
            //保存认证策略
            var modelStr = JsonConvert.SerializeObject(res.data);
            //加密保存起来
            var jmStr = DES3Encrypt.EncryptString(modelStr);
            var identity = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(KeyHelper.BbsUserKey,jmStr),
                }, BbsUserAuthorizeAttribute.BbsUserAuthenticationScheme)
            );
            await HttpContext.SignInAsync(BbsUserAuthorizeAttribute.BbsUserAuthenticationScheme, identity, new AuthenticationProperties
            {
                AllowRefresh = false
            });
            return Ok(res);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("reg")]
        public async Task<IActionResult> Reg([FromBody]List<FormParam> param)
        {
            if (param == null || param.Count < 4)
            {
                return Ok(new ApiResult<string>() {statusCode = 500, message = ApiEnum.ParameterError.GetEnumText()});
            }
            var model = new Core.Model.Member.Member()
            {
                LoginName = param[0].value,
                Grade = "d08b0d59-34a6-41d7-8bb0-6ddedd43ab33",
                NickName = param[0].value,
                Mobile = param[1].value,
                Email = param[2].value,
                LoginPwd = param[3].value,
                Autograph = "这家伙很懒，什么也没留下",
                HeadPic = "/dist_bbs/images/head/"+Utils.Number(1) + ".jpg"
            };
            return Ok(await _memberService.Add(model));
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("forgotpass")]
        public async Task<IActionResult> ForgotPassword([FromBody]List<FormParam> param)
        {
            if (param == null)
            {
                return Ok(new ApiResult<string>() { statusCode = 500, message = ApiEnum.ParameterError.GetEnumText() });
            }
           
            return Ok(new ApiResult<string>(){});
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("getuser")]
        public async Task<IActionResult> IsLogin()
        {
            var auth = await HttpContext.AuthenticateAsync(BbsUserAuthorizeAttribute.BbsUserAuthenticationScheme);
            if (auth.Succeeded)
            {
                var str = auth.Principal.Identities.First(u => u.IsAuthenticated).FindFirst(KeyHelper.BbsUserKey).Value;
                var model = JsonConvert.DeserializeObject<Core.Model.Member.Member>(DES3Encrypt.DecryptString(str));
                return Ok(new ApiResult<Core.Model.Member.Member>() { data = model });
            }
            else
            {
                return Ok(new ApiResult<string>() { statusCode=400 });
            }
        }

        /// <summary>
        /// 管理员退出
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [AllowAnonymous]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(BbsUserAuthorizeAttribute.BbsUserAuthenticationScheme);
            return Ok(new ApiResult<string>() { });
        }

        /// <summary>
        /// 保存用提交的问题
        /// </summary>
        /// <returns></returns>
        [HttpPost("save/question")]
        public async Task<IActionResult> SaveQuestion([FromBody]List<FormParam> param)
        {
            var u = GetLoginUser().Result;
            return Ok(await _questionService.Add(param,u.Guid));
        }

        /// <summary>
        /// 提交回答问题
        /// </summary>
        /// <returns></returns>
        [HttpPost("save/answer")]
        public async Task<IActionResult> SaveQuestion([FromBody]Bbs_Answer param)
        {
            param.Guid = Guid.NewGuid().ToString();
            param.UserGuid = GetLoginUser().Result.Guid;
            //修改问题的答案数量
            await _questionService.UpdateAsync(m => new Bbs_Questions(){AnswerSum = m.AnswerSum+1}, m => m.Guid == param.QuestionGuid);

            return Ok(await _answerService.AddAsync(param));
        }

        /// <summary>
        /// 返回登录人的信息
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task<Core.Model.Member.Member> GetLoginUser()
        {
            var auth = await HttpContext.AuthenticateAsync(BbsUserAuthorizeAttribute.BbsUserAuthenticationScheme);
            var str = auth.Principal.Identities.First(u => u.IsAuthenticated).FindFirst(KeyHelper.BbsUserKey).Value;
            return  JsonConvert.DeserializeObject<Core.Model.Member.Member>(DES3Encrypt.DecryptString(str));
        }

    }
}