using System.Collections.Generic;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Service.DtoModel;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /*!
    * 文件名称：Bbs_questions服务接口
    */
	public interface IBbs_QuestionsService: IBaseServer<Bbs_Questions>
	{
        /// <summary>
        /// 分页查询问题列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        Task<ApiResult<Page<Bbs_Questions>>> GetPageList(PageParm parm);

        /// <summary>
        /// 审核，失败发送原因
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Audit(QuestionAuditParam param);

        /// <summary>
        /// 前端提交发表问题
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Add(List<FormParam> param,string userGuid);

        /// <summary>
        /// 前台右侧内容
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<PageRightDto>> GetRgithData();

        /// <summary>
        /// 查询专家
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<MemberQuestion>>> GetPageExpert(PageParm param);

        /// <summary>
        /// 增加查看数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> AddLookSum(Bbs_Questions model);
    }
}