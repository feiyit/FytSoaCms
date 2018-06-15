using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Menu")]
    public class MenuController : Controller
    {
        private readonly ISysMenuService _sysMenuService;
        public MenuController(ISysMenuService sysMenuService)
        {
            _sysMenuService = sysMenuService;
        }

        /// <summary>
        /// 获得组织结构Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("gettree")]
        public List<SysMenuTree> GetListPage()
        {
            return _sysMenuService.GetListTreeAsync().Result.data;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getpages")]
        public async Task<JsonResult> GetPages(string key)
        {
            var res = await _sysMenuService.GetPagesAsync(key);
            if (res.data.Items.Count > 0)
            {
                foreach (var item in res.data.Items)
                {
                    item.Name = Utils.LevelName(item.Name, item.Layer);
                }
            }
            return Json(new { code = 0, msg = "success", count = res.data.TotalItems, data = res.data.Items });
        }

        /// <summary>
        /// 获得字典栏目Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> AddMenu(SysMenu parm)
        {
            return await _sysMenuService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> DeleteMenu(string parm)
        {
            return await _sysMenuService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> EditMenu(SysMenu parm)
        {
            return await _sysMenuService.ModifyAsync(parm);
        }
    }
}