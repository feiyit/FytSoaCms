using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FytSoa.Core.Model.Member;

namespace FytSoa.Api.Controllers.Member
{
    [JwtAuthorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMember_GroupService _groupService;
        public MemberController(IMemberService memberService, IMember_GroupService groupService)
        {
            _memberService = memberService;
            _groupService = groupService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<IActionResult> GetPages([FromQuery]PageParm parm)
        {
            return Ok(await _memberService.GetPageList(parm));
        }

        /// <summary>
        /// 根据ID查询内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("model")]
        public async Task<IActionResult> GetModel([FromBody]ParmString obj)
        {
            var res = await _memberService.GetModelAsync(m => m.Guid == obj.parm);
            if (!string.IsNullOrEmpty(res.data?.Guid))
            {
                res.data.LoginPwd= DES3Encrypt.DecryptString(res.data.LoginPwd);
            }
            //获得所有组
            var group = await _groupService.GetListAsync(m => !m.IsDel, m => m.Level, DbOrderEnum.Asc);
            return Ok(new ApiResult<MemberGroupDto>() { statusCode=res.statusCode,data = new MemberGroupDto() {  member=res.data,group=group.data} });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Core.Model.Member.Member model)
        {
            return Ok(await _memberService.Add(model));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody]Core.Model.Member.Member model)
        {
            return Ok(await _memberService.Edit(model));
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("status")]
        public async Task<IActionResult> Status([FromBody]PageParm obj)
        {
            var status = obj.types == 1 ? true : false;
            return Ok(await _memberService.UpdateAsync(m => new Core.Model.Member.Member() { Status = status }, m => m.Guid == obj.guid));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody]ParmString obj)
        {
            var list = Utils.StrToListString(obj.parm);
            return Ok(await _memberService.UpdateAsync(m => new Core.Model.Member.Member() { IsDel = true }, m => list.Contains(m.Guid)));
        }
    }
}