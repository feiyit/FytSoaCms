using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /*!
    * 文件名称：CmsArticle服务接口
    * 版权所有：北京飞易腾科技有限公司
    * 企业官网：http://www.feiyit.com
    */
	public interface ICmsArticleService: IBaseServer<CmsArticle>
	{
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm">CmsColumn</param>
        /// <returns></returns>
        Page<CmsArticle> GetList(PageParm parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        new Task<ApiResult<string>> UpdateAsync(CmsArticle parm);

        /// <summary>
        /// 转移到回收站
        /// </summary>
        /// <param name="parm">T</param>
        /// <param name="type">0=加入回收站  1=恢复</param>
        /// <returns></returns>
        Task<ApiResult<string>> GoRecycle(string parm,int type);

        /// <summary>
        /// 复制或转移
        /// </summary>
        /// <param name="parm">id集合</param>
        /// <param name="type">1=copy  2=转移</param>
        /// <param name="columnid">栏目id</param>
        /// <returns></returns>
        Task<ApiResult<string>> GoCopyOrTransfer(string parm, int type,int columnid);

        /// <summary>
        /// 查询网页案例和新闻
        /// </summary>
        /// <param name="parm">CmsColumn</param>
        /// <returns></returns>
        Page<CmsArticle> WebGetList(PageParm parm, List<int> columnList);

    }
}