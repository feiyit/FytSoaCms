using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Form;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Form
{
    [Route("api/[controller]")]
    [JwtAuthorize(Roles = "Admin")]
    public class FormController : ControllerBase
    {
        private readonly IFormTableService _tableService;
        public FormController(IFormTableService tableService)
        {
            _tableService = tableService;
        }

        /// <summary>
        /// 获得创建表信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParm param)
        {
            return Ok(await _tableService.GetPagesAsync(param));
        }

        /// <summary>
        /// 根据编号，获得创建表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("model")]
        public async Task<IActionResult> GetTableModel([FromBody]ParmString param)
        {
            return Ok(await _tableService.GetModelAsync(m=>m.Guid== param.parm));
        }

        /// <summary>
        /// 添加表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddTable([FromBody]FormTable model)
        {
            model.Guid = Guid.NewGuid().ToString();
            return Ok(await _tableService.AddAsync(model));
        }

        /// <summary>
        /// 修改表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<IActionResult> UpdateTable([FromBody]FormTable model)
        {
            model.UpdateDate = DateTime.Now;
            return Ok(await _tableService.UpdateAsync(model));
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="obj">obj.types</param>
        /// <returns></returns>
        [HttpPost("status")]
        public async Task<IActionResult> Status([FromBody]PageParm obj)
        {
            var model = await _tableService.GetModelAsync(m=>m.Guid==obj.guid);
            model.data.IsDel = obj.types == 1;
            return Ok(await _tableService.UpdateAsync(model.data));
        }
    }
}
