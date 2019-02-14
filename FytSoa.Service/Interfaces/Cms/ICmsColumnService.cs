using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /*!
    * 文件名称：CmsColumn服务接口
    * 版权所有：北京飞易腾科技有限公司
    * 企业官网：http://www.feiyit.com
    */
	public interface ICmsColumnService: IBaseServer<CmsColumn>
	{
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm">CmsColumn</param>
        /// <returns></returns>
        List<CmsColumn> RecursiveModule(List<CmsColumn> list);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        new Task<ApiResult<string>> UpdateAsync(CmsColumn parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        new Task<ApiResult<string>> AddAsync(CmsColumn parm);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="p">父级</param>
        /// <param name="i">当前id</param>
        /// <param name="o">排序方式</param>
        /// <returns></returns>
        Task<ApiResult<string>> ColSort(int p,int i,int o);

        /// <summary>
        /// 栏目Tree
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        Task<ApiResult<List<ColumnTree>>> TreeAsync(int type);
    }
}