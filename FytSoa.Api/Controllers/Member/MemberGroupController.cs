using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Member;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Member
{
    [JwtAuthorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberGroupController : ControllerBase
    {
        private readonly IMember_GroupService _groupService;
        public MemberGroupController(IMember_GroupService groupService)
        {
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
            return Ok(await _groupService.GetPagesAsync(parm, m => !m.IsDel, m => m.Level, DbOrderEnum.Asc));
        }

        /// <summary>
        /// 获得所有组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _groupService.GetListAsync(m => !m.IsDel, m => m.Level, DbOrderEnum.Asc));
        }

        /// <summary>
        /// 根据ID查询内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("model")]
        public async Task<IActionResult> GetPages([FromBody]ParmString obj)
        {
            return Ok(await _groupService.GetModelAsync(m=>m.Guid==obj.parm));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Member_Group model)
        {
            model.Guid = Guid.NewGuid().ToString();
            return Ok(await _groupService.AddAsync(model));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody]Member_Group model)
        {
            return Ok(await _groupService.UpdateAsync(model));
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
            return Ok(await _groupService.UpdateAsync(m=>new Member_Group() { Status= status },m=>m.Guid==obj.guid));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody]ParmString obj)
        {
            return Ok(await _groupService.DeleteAsync(obj.parm));
        }
    }
}