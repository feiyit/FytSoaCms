using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Bbs
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtAuthorize(Roles = "Admin")]
    public class QuestionController : ControllerBase
    {
        private readonly IBbs_QuestionsService _questionService;
        public QuestionController(IBbs_QuestionsService questionService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="objPram"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetPages([FromQuery]PageParm objPram)
        {
            return Ok(await _questionService.GetPageList(objPram));
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("status")]
        public async Task<IActionResult> Status([FromBody]PageParm obj)
        {
            var IsRed = obj.types == 1 ? true : false;
            return Ok(await _questionService.UpdateAsync(m => new Bbs_Questions()  { IsRed = IsRed }, m => m.Guid == obj.guid));
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("audit")]
        public async Task<IActionResult> Delete([FromBody]QuestionAuditParam param)
        {
            return Ok(await _questionService.Audit(param));
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody]ParmString obj)
        {
            var list = Utils.StrToListString(obj.parm);
            return Ok(await _questionService.DeleteAsync(m => list.Contains(m.Guid)));
        }
    }
}