using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class TagsController : ControllerBase
    {
        private readonly IBbs_TagsService _tagService;
        public TagsController(IBbs_TagsService tagService)
        {
            _tagService = tagService;
        }

        #region 社区-标签
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetPages([FromQuery]PageParm parm)
        {
            return Ok(await _tagService.GetPagesAsync(parm,m=>!m.IsDel,m=>m.FirstLetter,DbOrderEnum.Asc));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Bbs_Tags model)
        {
            model.Guid = Guid.NewGuid().ToString();
            //获得首字母
            model.FirstLetter = model.EnTagName.Substring(0, 1).ToUpper();
            return Ok(await _tagService.AddAsync(model));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody]Bbs_Tags model)
        {
            //获得首字母
            model.FirstLetter = model.EnTagName.Substring(0, 1).ToUpper();
            return Ok(await _tagService.UpdateAsync(model));
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
            return Ok(await _tagService.UpdateAsync(m => new Bbs_Tags() { Status = status }, m => m.Guid == obj.guid));
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
            return Ok(await _tagService.UpdateAsync(m => new Bbs_Tags() { IsDel = true }, m => list.Contains(m.Guid)));
        }
        #endregion

        #region 社区过滤关键字
        /// <summary>
        /// 获得过滤关键字
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("get/key")]
        public IActionResult GetFilterKey()
        {
            var key = RedisHelper.Get<FilterKey>(KeyHelper.FilterKey);
            if (key==null)
            {
                key = new FilterKey();
            }
            return Ok(new ApiResult<FilterKey>() { data= key });
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("save/key")]
        public IActionResult SaveFilterKey(FilterKey parm)
        {
            RedisHelper.Set(KeyHelper.FilterKey, parm);
            return Ok(new ApiResult<string>() { });
        }
        #endregion
    }
}