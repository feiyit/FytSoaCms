﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Wx;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Wx
{
    [Produces("application/json")]
    [Route("api/wx/setting")]
    [JwtAuthorize(Roles = "Admin")]
    //[ApiController]
    public class WxSettingController : ControllerBase
    {
        private readonly IWxSettingService _settingService;
        public WxSettingController(IWxSettingService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<IActionResult> GetPages([FromQuery]PageParm parm)
        {
            var res = await _settingService.GetPagesAsync(parm, m => true, m => m.AddDate, DbOrderEnum.Desc);
            return Ok(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("add"), Log("WxSetting：add", LogType = LogEnum.ADD)]
        public async Task<IActionResult> AddSetting([FromBody]WxSetting parm)
        {
            return Ok(await _settingService.AddAsync(parm));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit"), Log("WxSetting：edit", LogType = LogEnum.UPDATE)]
        public async Task<IActionResult> EditSetting([FromBody]WxSetting parm)
        {
            return Ok(await _settingService.UpdateAsync(parm));
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete"), Log("WxSetting：delete", LogType = LogEnum.DELETE)]
        public async Task<IActionResult> DeleteRole([FromBody]ParmString obj)
        {
            return Ok(await _settingService.DeleteAsync(obj.parm));
        }
    }
}