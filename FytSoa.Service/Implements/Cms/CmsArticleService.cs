using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;

namespace FytSoa.Service.Implements
{
    /*!
    * 文件名称：CmsArticle服务接口实现
    * 版权所有：北京飞易腾科技有限公司
    * 企业官网：http://www.feiyit.com
    */
    public class CmsArticleService : BaseServer<CmsArticle>, ICmsArticleService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Page<CmsArticle> GetList(PageParm parm)
        {
            return Db.Queryable<CmsArticle>()
                .WhereIF(parm.id!=0,m=>m.ColumnId==parm.id)
                .WhereIF(!string.IsNullOrEmpty(parm.key),m=>m.Title.Contains(parm.key) || m.Tag.Contains(parm.key) || m.Summary.Contains(parm.key))
                .WhereIF(parm.audit == 0,m=>m.Audit)
                .WhereIF(parm.audit == 1, m => !m.Audit)
                .WhereIF(parm.types == 1, m => !m.IsRecyc)
                .WhereIF(parm.types == 0, m => m.IsRecyc)
                .WhereIF(!string.IsNullOrEmpty(parm.where),parm.where)
                .OrderBy(m=>m.Sort,SqlSugar.OrderByType.Desc)
                .OrderBy(m=>m.EditDate,SqlSugar.OrderByType.Desc)
                .ToPage(parm.page, parm.limit);
        }

        /// <summary>
        /// 转移到回收站
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> GoRecycle(string parm, int type)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var list = Utils.StrToListInt(parm);
                var dbres = 0;
                if (type == 0)
                {
                    dbres= Db.Updateable<CmsArticle>().UpdateColumns(m => m.IsRecyc == true).Where(m => list.Contains(m.Id)).ExecuteCommand();
                }
                else
                {
                    dbres = Db.Updateable<CmsArticle>().UpdateColumns(m => m.IsRecyc == false).Where(m => list.Contains(m.Id)).ExecuteCommand();
                }
                if (dbres==0)
                {
                    res.message = "删除数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public new async Task<ApiResult<string>> UpdateAsync(CmsArticle parm)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                Db.Updateable(parm).IgnoreColumns(m => new { m.LastHitDate, m.AddDate,m.DelDate }).ExecuteCommand();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 复制或转移
        /// </summary>
        /// <param name="parm">id集合</param>
        /// <param name="type">1=copy  2=转移</param>
        /// <param name="columnid">栏目id</param>
        /// <returns></returns>
        public async Task<ApiResult<string>> GoCopyOrTransfer(string parm, int type, int columnid)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var list = Utils.StrToListInt(parm);
                if (type==1)
                {
                    //复制
                    var articleList = CmsArticleDb.GetList(m => list.Contains(m.Id));
                    foreach (var item in articleList)
                    {
                        item.Id = 0;
                        item.ColumnId = columnid;
                    }
                    CmsArticleDb.InsertRange(articleList.ToArray());
                }
                else
                {
                    //转移
                    Db.Updateable<CmsArticle>().UpdateColumns(m=>m.ColumnId==columnid).Where(m=>list.Contains(m.Id)).ExecuteCommand();
                }
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 查询网页案例和新闻
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Page<CmsArticle> WebGetList(PageParm parm,List<int> columnList)
        {
            return Db.Queryable<CmsArticle>()
                .WhereIF(parm.id != 0, m => m.ColumnId == parm.id)
                .WhereIF(columnList.Count>0, m=>columnList.Contains(m.ColumnId))
                .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Title.Contains(parm.key) || m.Tag.Contains(parm.key) || m.Summary.Contains(parm.key))
                .WhereIF(parm.audit == 0, m => m.Audit)
                .WhereIF(parm.audit == 1, m => !m.Audit)
                .WhereIF(parm.types == 1, m => !m.IsRecyc)
                .WhereIF(parm.types == 0, m => m.IsRecyc)
                .WhereIF(!string.IsNullOrEmpty(parm.where), parm.where)
                .OrderBy(m => m.Sort, OrderByType.Desc)
                .OrderBy(m => m.EditDate, OrderByType.Desc)
                .ToPage(parm.page, parm.limit);
        }
    }
}