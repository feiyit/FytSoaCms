using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers.Cms
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly ICmsMessageService _messageService;
        public MessageController(ICmsMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("page")]
        public async Task<ApiResult<Page<CmsMessage>>> GetPages(PageParm parm)
        {
            return await _messageService.GetPagesAsync(parm);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ApiResult<string>> Delete(string parm,int type=0)
        {
            if (type == 0)
            {
                return await _messageService.DeleteAsync(parm);
            }
            else
            {
                return await _messageService.DeleteAsync(m=>true);
            }
        }

        /// <summary>
        /// 改为已读
        /// </summary>
        /// <returns></returns>
        [HttpPost("read")]
        public async Task<ApiResult<string>> Read(int parm,int type=0)
        {
            if (type == 0)
            {
                return await _messageService.UpdateAsync(m => new CmsMessage() { Status = true }, m => m.Id == parm);
            }
            else
            {
                return await _messageService.UpdateAsync(m => new CmsMessage() { Status = true }, m => true);
            }
        }
    }
}