using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Cms
{
    [Authorize]
    public class ArticleModifyModel : PageModel
    {
        private readonly ICmsColumnService _columnService;
        private readonly ICmsArticleService _articleervice;
        public ArticleModifyModel(ICmsColumnService columnService, ICmsArticleService articleervice)
        {
            _columnService = columnService;
            _articleervice = articleervice;
        }

        [BindProperty]
        public CmsArticle Article { get; set; }

        public List<CmsColumn> ColumnList { get; set; }

        public void OnGet(int id = 0, int column = 0)
        {
            Article = _articleervice.GetModelAsync(m => m.Id == id).Result.data;
            if (Article.Id == 0 && column != 0)
            {
                Article.ColumnId = column;
            }
            var list = _columnService.RecursiveModule(_columnService.GetListAsync().Result.data);
            foreach (var item in list)
            {
                item.Title = Utils.LevelName(item.Title, item.ClassLayer);
            }
            ColumnList = list;
        }
    }
}