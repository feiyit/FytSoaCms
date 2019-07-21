using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FytSoa.Web.Pages.Bbs
{
    [BbsUserAuthorize]
    public class AskQuestionModel : PageModel
    {
        private readonly IBbs_ClassifyService _classifyService;
        private readonly IBbs_TagsService _tagService;
        public AskQuestionModel(IBbs_ClassifyService classifyService
        , IBbs_TagsService tagService)
        {
            _classifyService = classifyService;
            _tagService = tagService;
        }

        public List<Bbs_Classify> classifyList { get; set; }

        public string TagStr { get; set; }

        public void OnGet()
        {
            classifyList = _classifyService.GetListAsync(m => !m.IsDel, m => m.FirstLetter, DbOrderEnum.Asc).Result.data;

            var tagList = _tagService.GetListAsync().Result.data;
            if (tagList.Any())
            {
                TagStr = JsonConvert.SerializeObject(tagList.Select(m => new TagsDto(){Name = m.TagName,FirstLetter = m.FirstLetter}));
            }
        }
    }
}