using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Service.DtoModel;

namespace FytSoa.Service.Interfaces
{
    /*!
    * 文件名称：Bbs_answer服务接口
    */
	public interface IBbs_AnswerService: IBaseServer<Bbs_Answer>
	{
        /// <summary>
        /// 分页查询问题列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<Page<Bbs_Answer>>> GetPageUser(PageParm param);

        /// <summary>
        /// 用户详细里面的回答列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<Page<AnswerDto>>> GetUserCenterAnswer(PageParm param);
    }
}