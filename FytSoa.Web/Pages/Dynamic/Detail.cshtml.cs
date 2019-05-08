using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.Dynamic
{
    public class DetailModel : PageModel
    {
        private readonly ICacheService _cacheService;
        private readonly ICmsSiteService _siteService;
        private readonly ICmsColumnService _columnService;
        private readonly ICmsArticleService _articleService;
        public DetailModel(ICacheService cacheServicee, ICmsSiteService siteService, ICmsColumnService columnService
            , ICmsArticleService articleService)
        {
            _cacheService = cacheServicee;
            _siteService = siteService;
            _columnService = columnService;
            _articleService = articleService;
        }
        public CmsSite Site { get; set; }
        List<CmsColumn> Column { get; set; }
        public CmsArticle Article { get; set; }
        public CmsColumn ParentColumn { get; set; }
        public List<CmsArticle> ArticleList { get; set; }
        public void OnGet(int id)
        {
            //获得站点信息
            if (_cacheService.Exists(CacheKey.WEBCMSSITE))
            {
                Site = _cacheService.GetCache<CmsSite>(CacheKey.WEBCMSSITE);
            }
            else
            {
                Site = _siteService.GetModelAsync(m => m.Guid == "78756a6c-50c8-47a5-b898-5d6d24a20327").Result.data;
                //加入到缓存
                _cacheService.SetCache(CacheKey.WEBCMSSITE, Site, DateTimeOffset.Now.AddDays(30));
            }
            //获得栏目信息
            if (_cacheService.Exists(CacheKey.WEBCMSCOLUMN))
            {
                Column = _cacheService.GetCache<List<CmsColumn>>(CacheKey.WEBCMSCOLUMN);
            }
            else
            {
                Column = _columnService.GetListAsync(m => true, m => m.Sort, DbOrderEnum.Asc).Result.data;
                //加入到缓存
                _cacheService.SetCache(CacheKey.WEBCMSCOLUMN, Column, DateTimeOffset.Now.AddDays(30));
            }
            Article = _articleService.GetModelAsync(m => m.Id == id).Result.data;
            ParentColumn = Column.Find(m => m.Id == Article.ColumnId);
            #region 增加点击量
            //判断是否为当前天
            if (Convert.ToDateTime(Article.LastHitDate).Day == DateTime.Now.Day)
            {
                Article.DayHits += 1;
            }
            //判断是否为当前星期
            if (Convert.ToDateTime(Article.LastHitDate).DayOfWeek == DateTime.Now.DayOfWeek)
            {
                Article.WeedHits += 1;
            }
            //判断是否为当前天月份
            if (Convert.ToDateTime(Article.LastHitDate).Month == DateTime.Now.Month)
            {
                Article.MonthHits += 1;
            }
            Article.Hits += 1;
            Article.LastHitDate = DateTime.Now;
            _articleService.UpdateAsync(Article);
            #endregion

            #region 查询相关的新闻
            ArticleList = _articleService.GetPagesAsync(new Service.DtoModel.PageParm() { limit = 6 },
                m => m.Audit && !m.IsRecyc && m.ColumnId == Article.ColumnId && m.Id != Article.Id,
                m => m.Sort, DbOrderEnum.Desc).Result.data.Items;
            #endregion
        }
    }
}