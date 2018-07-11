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

namespace FytSoa.Api.Areas.APP.Controllers
{
    [Route("app/api/[controller]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IErpShopsService _shopsService;
        private readonly IErpStaffService _staffService;
        private readonly IErpShopUserService _userService;
        private readonly IErpPushService _pushService;
        private readonly IErpAppSettingService _settingService;
        public UserController(IErpShopsService shopsService, IErpStaffService staffService,
            IErpShopUserService userService, IErpPushService pushService,
            IErpAppSettingService settingService)
        {
            _shopsService = shopsService;
            _staffService = staffService;
            _userService = userService;
            _pushService = pushService;
            _settingService = settingService;
        }

        /// <summary>
        /// 员工登录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ApiResult<ShopBasicDto>> LoginAsync(StaffLoginDto parm)
        {
            return await _staffService.LoginAsync(parm);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("editpwd")]
        public async Task<ApiResult<string>> StaffEditPwdAsync(StaffModifyPwdParm parm)
        {
            return await _staffService.ModifyLoginPwdAsync(parm);
        }

        /// <summary>
        /// 店铺的店员列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("stafflist")]
        public JsonResult StaffListAsync(PageParm parm)
        {
            var res = _staffService.GetPagesAsync(parm).Result;
            var list = res.data.Items?.Select(m=>new {
                m.Guid,m.TrueName,m.Sex,m.Mobile
            });            
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count??0, data = list });
        }

        /// <summary>
        /// 店铺的店员修改
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("staffedit")]
        public async Task<ApiResult<string>> StaffModifyAsync(ErpStaff parm)
        {
            return await _staffService.ModifyAsync(parm);
        }

        /// <summary>
        /// 店铺的店员修改
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("staffadd")]
        public async Task<ApiResult<string>> AtaffAddAsync(ErpStaff parm)
        {
            return await _staffService.AddAsync(parm);
        }

        /// <summary>
        /// 店铺的会员列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("userlist")]
        public JsonResult UserListAsync(PageParm parm)
        {
            var res = _userService.GetPagesAsync(parm).Result;
            var list = res.data.Items?.Select(m => new {
                m.Guid,
                m.Mobile,
                m.Sex,
                m.UserName,
                m.Points,
                Birthday=Convert.ToDateTime(m.Birthday).ToShortDateString().Replace("/","-")
            });
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count??0, data = list });
        }

        /// <summary>
        /// 店铺的会员修改
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("useredit")]
        public async Task<ApiResult<string>> UserModifyAsync(ErpShopUser parm)
        {
            return await _userService.ModifyAsync(parm);
        }

        /// <summary>
        /// 店铺的会员修改
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("useradd")]
        public async Task<ApiResult<string>> UserAddAsync(ErpShopUser parm)
        {
            return await _userService.AddAsync(parm);
        }

        /// <summary>
        /// 系统消息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("message")]
        public JsonResult MessageListAsync(PageParm parm)
        {
            var res = _pushService.GetPagesAsync(parm).Result;            
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count ?? 0, data = res.data.Items });
        }

        /// <summary>
        /// 版本更新
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost("version")]
        public JsonResult AppVersionAsync(PageParm parm)
        {
            parm.limit = 1;
            var res = _settingService.GetPagesAsync(parm).Result;
            return Json(new { statusCode = 200, msg = "success", count = res.data.Items?.Count ?? 0, data = res.data.Items });
        }
    }
}