using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShopsController : Controller
    {
        private readonly IErpShopsService _shopsService;
        private readonly IErpStaffService _staffService;
        private readonly IErpShopUserService _shopUserService;
        private readonly IErpShopActivityService _activityService;
        private readonly IErpPushService _pushService;
        public ShopsController(IErpShopsService shopsService, IErpStaffService staffService, IErpShopUserService shopUserService,
            IErpShopActivityService activityService, IErpPushService pushService)
        {
            _shopsService = shopsService;
            _staffService = staffService;
            _shopUserService = shopUserService;
            _activityService = activityService;
            _pushService = pushService;
        }

        #region 店铺API
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<JsonResult> GetPages(PageParm parm)
        {
            var res = await _shopsService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.Items?.Count, data = res.data.Items });
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ApiResult<string>> AddShop(ErpShops parm)
        {
            return await _shopsService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> DeleteShop(string parm)
        {
            return await _shopsService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ApiResult<string>> EditShop(ErpShops parm)
        {
            return await _shopsService.ModifyAsync(parm);
        }
        #endregion

        #region 店铺员工API
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("stafflist")]
        public async Task<JsonResult> GetStaffPages(PageParm parm)
        {
            var res = await _staffService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.Items?.Count, data = res.data.Items });
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("addstaff")]
        public async Task<ApiResult<string>> AddStaff(ErpStaff parm)
        {
            return await _staffService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deletestaff")]
        public async Task<ApiResult<string>> DeleteStaff(string parm)
        {
            return await _staffService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editstaff")]
        public async Task<ApiResult<string>> EditStaff(ErpStaff parm)
        {
            return await _staffService.ModifyAsync(parm);
        }
        #endregion

        #region 店铺会员API
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("userlist")]
        public async Task<JsonResult> GetUserPages(PageParm parm)
        {
            var res = await _shopUserService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.Items?.Count, data = res.data.Items });
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("adduser")]
        public async Task<ApiResult<string>> AddUser(ErpShopUser parm)
        {
            return await _shopUserService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deleteuser")]
        public async Task<ApiResult<string>> DeleteUser(string parm)
        {
            return await _shopUserService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("edituser")]
        public async Task<ApiResult<string>> EditUser(ErpShopUser parm)
        {
            return await _shopUserService.ModifyAsync(parm);
        }
        #endregion

        #region 店铺活动API
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("actlist")]
        public async Task<JsonResult> GetActPages(PageParm parm)
        {
            var res = await _activityService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.Items?.Count, data = res.data?.Items });
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("addact")]
        public async Task<ApiResult<string>> AddAct(ErpShopActivity parm, ShopActivityParm fullParm)
        {
            return await _activityService.AddAsync(parm, fullParm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deleteact")]
        public async Task<ApiResult<string>> DeleteAct(string parm)
        {
            return await _activityService.DeleteAsync(parm);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("editact")]
        public async Task<ApiResult<string>> EditAct(ErpShopActivity parm, ShopActivityParm fullParm)
        {
            return await _activityService.ModifyAsync(parm, fullParm);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        [HttpPost("modifyactstatus")]
        public async Task<ApiResult<string>> ModifyStatusAsync(ErpShopActivity parm)
        {
            return await _activityService.ModifyStatusAsync(parm);
        }
        #endregion

        #region 消息推送
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("pushlist")]
        public async Task<JsonResult> GetPushPages(PageParm parm)
        {
            var res = await _pushService.GetPagesAsync(parm);
            return Json(new { code = 0, msg = "success", count = res.data.Items?.Count, data = res.data.Items });
        }

        /// <summary>
        /// 添加一条菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost("addpush")]
        public async Task<ApiResult<string>> AddPush(ErpPush parm)
        {
            return await _pushService.AddAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("deletepush")]
        public async Task<ApiResult<string>> DeletePush(string parm)
        {
            return await _pushService.DeleteAsync(parm);
        }
        #endregion
    }
}