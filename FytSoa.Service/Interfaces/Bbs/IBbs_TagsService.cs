using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Service.DtoModel;

namespace FytSoa.Service.Interfaces
{
    /*!
    * 文件名称：Bbs_tags服务接口
    */
	public interface IBbs_TagsService: IBaseServer<Bbs_Tags>
    {
        /// <summary>
        /// 查询所有标签，带数量
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<TagsDto>>> GetListTagCounts();
    }
}