using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.Bbs
{
    public class TagsModel : PageModel
    {
        private readonly IBbs_TagsService _tagService;
        public TagsModel(IBbs_TagsService tagService)
        {
            _tagService = tagService;
        }

        public List<TagsDto> TagList { get; set; }

        public void OnGet()
        {
            TagList = _tagService.GetListTagCounts().Result.data;
        }
    }
}