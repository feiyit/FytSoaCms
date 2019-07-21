using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;

namespace FytSoa.Service.Implements
{
    /*!
    * 文件名称：Bbs_tags服务接口实现
    */
    public class Bbs_TagsService : BaseServer<Bbs_Tags>, IBbs_TagsService
    {
        /// <summary>
        /// 查询所有标签，带数量
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<TagsDto>>> GetListTagCounts()
        {
            var res = new ApiResult<List<TagsDto>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data=await Db.Queryable<Bbs_Tags>()
                    .Where(s => !s.IsDel)
                    .Select(s => new TagsDto()
                    {
                        Name = s.TagName,
                        EnTagName=s.EnTagName,
                        FirstLetter = s.FirstLetter,
                        TagCount = SqlFunc.Subqueryable<Bbs_Questions>().Where(g => g.Tags.Contains(s.TagName)).Count()
                    }).OrderBy(g => g.TagCount, OrderByType.Desc)
                    .Take(8).ToListAsync();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }

            return res;
        }
    }
}