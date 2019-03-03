using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Wx;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Wx
{
    [Produces("application/json")]
    [Route("api/wx/material")]
    [Authorize(Roles = "Admin")]
    public class WxMaterialController : Controller
    {
        private readonly IWxMaterialService _meterialService;
        public WxMaterialController(IWxMaterialService meterialService)
        {
            _meterialService = meterialService;
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("list")]
        public async Task<ApiResult<Page<WxMaterial>>> GetList(PageParm parm)
        {
            return await _meterialService.GetPageList(parm);
        }

        /// <summary>
        /// 添加一条微信素材
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> Add(WxMaterial model)
        {
            return await _meterialService.Add(model,null);
        }

        /// <summary>
        ///删除一条微信素材
        /// </summary>
        /// <returns></returns>
        [HttpPost("del")]
        public async Task<ApiResult<string>> Delete(int parm)
        {
            return await _meterialService.DeleteAsync(m=>m.Id== parm);
        }

        /// <summary>
        /// 获得一条微信素材
        /// </summary>
        /// <returns></returns>
        [HttpPost("get")]
        public async Task<ApiResult<WxMaterial>> GetModel(int id)
        {
            return await _meterialService.GetModelAsync(m => m.Id == id);
        }
    }
}