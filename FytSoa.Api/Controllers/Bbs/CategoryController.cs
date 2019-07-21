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
    [Route("api/bbs/[controller]")]
    [ApiController]
    [JwtAuthorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly IBbs_ClassifyService _classService;
        public CategoryController(IBbs_ClassifyService classService)
        {
            _classService = classService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetPages([FromQuery]PageParm parm)
        {
            return Ok(await _classService.GetPagesAsync(parm, m => !m.IsDel, m => m.FirstLetter, DbOrderEnum.Asc));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Bbs_Classify model)
        {
            model.Guid = Guid.NewGuid().ToString();
            //获得首字母
            model.FirstLetter = model.EnClassName.Substring(0, 1).ToUpper();
            return Ok(await _classService.AddAsync(model));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody]Bbs_Classify model)
        {
            //获得首字母
            model.FirstLetter = model.EnClassName.Substring(0, 1).ToUpper();
            return Ok(await _classService.UpdateAsync(model));
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
            return Ok(await _classService.UpdateAsync(m => new Bbs_Classify() { Status = status }, m => m.Guid == obj.guid));
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
            return Ok(await _classService.UpdateAsync(m => new Bbs_Classify() { IsDel = true }, m => list.Contains(m.Guid)));
        }
    }
}