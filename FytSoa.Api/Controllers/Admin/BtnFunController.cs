using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class BtnFunController : Controller
    {
        private readonly ISysBtnFunService _btnFunService;
        public BtnFunController(ISysBtnFunService btnFunService)
        {
            _btnFunService = btnFunService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<JsonResult> GetPages(string key,string menuGuid)
        {
            var res = await _btnFunService.GetPagesAsync(key,menuGuid);
            return Json(new { code = 0, msg = "success", count = res.data.Items.Count, data=res.data.Items });
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> AddBtnFun(SysBtnFun parm)
        {
            return await _btnFunService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> DeleteBtnFun(string parm)
        {
            var list = Utils.StrToListString(parm);
            return await _btnFunService.DeleteAsync(m => list.Contains(m.Guid));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> EditBtnFun(SysBtnFun parm)
        {
            return await _btnFunService.UpdateAsync(parm);
        }
    }
}