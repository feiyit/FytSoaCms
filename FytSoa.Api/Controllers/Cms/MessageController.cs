using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Cms
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [JwtAuthorize(Roles = "Admin")]
    public class MessageController : ControllerBase
    {
        private readonly ICmsMessageService _messageService;
        public MessageController(ICmsMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("page")]
        public async Task<IActionResult> GetPages([FromBody]PageParm parm)
        {
            parm.site = parm.site = SiteTool.CurrentSite?.Guid; ;
            return Ok(await _messageService.GetPagesAsync(parm, m => m.SiteGuid == parm.site, m => m.AddDate, DbOrderEnum.Desc));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody]MessageDeleteDto obj)
        {
            if (obj.type == 0)
            {
                return Ok(await _messageService.DeleteAsync(obj.parm));
            }
            else
            {
                return Ok(await _messageService.DeleteAsync(m => true));
            }
        }

        /// <summary>
        /// 改为已读
        /// </summary>
        /// <returns></returns>
        [HttpPost("read")]
        public async Task<IActionResult> Read([FromBody]MessageReadDto obj)
        {
            if (obj.type == 0)
            {
                return Ok(await _messageService.UpdateAsync(m => new CmsMessage() { Status = true }, m => m.Id == obj.parm));
            }
            else
            {
                return Ok(await _messageService.UpdateAsync(m => new CmsMessage() { Status = true }, m => true));
            }
        }
    }
}