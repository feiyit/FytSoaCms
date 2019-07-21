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
    public class ExpertModel : PageModel
    {
        private readonly IBbs_QuestionsService _questionService;
        public ExpertModel(IBbs_QuestionsService questionService)
        {
            _questionService = questionService;
        }

        public int pageIndex { get; set; }
        public int Types { get; set; } = 0;
        public Page<MemberQuestion> UserList { get; set; }

        public void OnGet(int type=1,string key=null)
        {
            int page = 1;
            var listPage = Request.Query["page"];
            if (!string.IsNullOrEmpty(listPage))
            {
                page = Convert.ToInt32(listPage);
            }
            pageIndex = page;
            Types = type;
            UserList = _questionService.GetPageExpert(new PageParm(){page = page,types = type,key = key}).Result.data;
        }
    }
}