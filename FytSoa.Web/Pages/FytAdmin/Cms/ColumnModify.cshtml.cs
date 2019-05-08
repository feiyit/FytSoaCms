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
    public class ColumnModifyModel : PageModel
    {
        private readonly ICmsColumnService _columnService;
        private readonly ICmsTemplateService _templateService;
        public ColumnModifyModel(ICmsColumnService columnService, ICmsTemplateService templateService)
        {
            _columnService = columnService;
            _templateService = templateService;
        }

        [BindProperty]
        public CmsColumn Column { get; set; }

        public List<CmsTemplate> TempList { get; set; }

        public List<CmsColumn> ColumnList { get; set; }

        public void OnGet(int id=0,int parent=0)
        {
            Column = _columnService.GetModelAsync(m => m.Id ==id).Result.data;
            if (Column.Id==0 && parent!=0)
            {
                Column.ParentId = parent;
            }
            TempList = _templateService.GetListAsync(m => true, m => m.AddDate, Common.DbOrderEnum.Asc).Result.data;
            var list = _columnService.RecursiveModule(_columnService.GetListAsync().Result.data);
            foreach (var item in list)
            {
                item.Title = Utils.LevelName(item.Title, item.ClassLayer);
            }
            ColumnList = list;
        }
    }
}