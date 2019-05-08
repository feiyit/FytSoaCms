using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Cms
{
    [Authorize]
    public class DownloadModel : PageModel
    {
        private readonly ICmsDownloadService _downService;
        public DownloadModel(ICmsDownloadService downService)
        {
            _downService = downService;
        }

        public int ColumnId { get; set; }

        public void OnGet(int id = 0, int column = 0)
        {
            ColumnId = column;
        }
    }
}