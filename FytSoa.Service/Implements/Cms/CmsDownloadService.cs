using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
namespace FytSoa.Service.Implements
{
    /*!
    * 文件名称：CmsDownload服务接口实现
    * 版权所有：北京飞易腾科技有限公司
    * 企业官网：http://www.feiyit.com
    */
	public class CmsDownloadService : BaseServer<CmsDownload>, ICmsDownloadService
	{
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Page<CmsDownload> GetList(PageParm parm)
        {
            return Db.Queryable<CmsDownload>()
                .WhereIF(parm.id != 0, m => m.ColumnId == parm.id)
                .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Title.Contains(parm.key) || m.Tag.Contains(parm.key) || m.Summary.Contains(parm.key))
                .WhereIF(parm.audit == 0, m => m.Audit)
                .WhereIF(parm.audit == 1, m => !m.Audit)
                .WhereIF(!string.IsNullOrEmpty(parm.where), parm.where)
                .OrderBy(m => m.Sort, SqlSugar.OrderByType.Desc)
                .OrderBy(m => m.EditDate, SqlSugar.OrderByType.Desc)
                .ToPage(parm.page, parm.limit);
        }

    }
}