using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.Cases
{
    public class IndexModel : PageModel
    {
        private readonly ICacheService _cacheService;
        private readonly ICmsSiteService _siteService;
        private readonly ICmsColumnService _columnService;
        private readonly ICmsArticleService _articleService;
        public IndexModel(ICacheService cacheServicee, ICmsSiteService siteService, ICmsColumnService columnService
            , ICmsArticleService articleService)
        {
            _cacheService = cacheServicee;
            _siteService = siteService;
            _columnService = columnService;
            _articleService = articleService;
        }
        public CmsSite Site { get; set; }
        public List<CmsColumn> Column { get; set; }
        public List<CmsArticle> Case { get; set; }
        public CmsColumn ActiveColumn { get; set; }
        public Page<CmsArticle> Page { get; set; }
        public void OnGet(string type)
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
            int page = 1;
            var listPage = Request.Query["page"];
            if (!string.IsNullOrEmpty(listPage))
            {
                page = Convert.ToInt32(listPage);
            }

            if (!string.IsNullOrEmpty(type))
            {
                ActiveColumn = Column.Find(m => m.EnTitle == type);
                Page = _articleService.GetList(new Service.DtoModel.PageParm() { page = page, limit = 12, types = 1, id = ActiveColumn.Id });
                Case = Page.Items;
            }
            else
            {
                ActiveColumn = new CmsColumn()
                {
                    Id = 0,
                    Title = "飞易腾案例"
                };
                ActiveColumn.Id = 0;
                var caseColumn = Column.Where(m => m.ParentId == 1015).Select(m => m.Id).ToList();
                Page = _articleService.WebGetList(new Service.DtoModel.PageParm() { page = page, limit = 12, types = 1 }, caseColumn);
                Case = Page.Items;
            }
        }
    }
}