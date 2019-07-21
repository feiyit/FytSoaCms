using System.Collections.Generic;
using FytSoa.Common;
using FytSoa.Core.Model.Member;
using FytSoa.Service.DtoModel;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /*!
    * 文件名称：Bbs_user服务接口
    */
	public interface IMemberService : IBaseServer<Member>
	{
        /// <summary>
        /// 查询用户信息-增加组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<Page<Member>>> GetPageList(PageParm parm);

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Add(Member model);

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Edit(Member model);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<Member>> Login(List<FormParam> param);

    }
}