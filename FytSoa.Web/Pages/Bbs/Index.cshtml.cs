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
using Newtonsoft.Json;

namespace FytSoa.Web.Pages.Bbs
{
    public class IndexModel : PageModel
    {
        private readonly IBbs_ClassifyService _classifyService;
        private readonly IBbs_QuestionsService _askService;
        public IndexModel(IBbs_ClassifyService classifyService
        , IBbs_QuestionsService askService)
        {
            _classifyService = classifyService;
            _askService = askService;
        }

        public List<Bbs_Classify> classifyList { get; set; }

        /// <summary>
        /// 条件类型-提供页面绑定
        /// </summary>
        public string Types { get; set; }

        /// <summary>
        /// 分页当前码
        /// </summary>
        public int pageIndex { get; set; } = 0;

        /// <summary>
        /// 分页当前码
        /// </summary>
        public int Limit { get; set; } = 5;

        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }=null;

        /// <summary>
        /// 问题列表
        /// </summary>
        public Page<Bbs_Questions> Ask { get; set; }

        /// <summary>
        /// 下拉列表
        /// </summary>
        public List<int> HtmlLimit => new List<int>(){5,10,12,15,20};

        public void OnGet(string category=null,string key=null,string where="",int limit=5)
        {
            int page = 1;
            var listPage = Request.Query["page"];
            if (!string.IsNullOrEmpty(listPage))
            {
                page = Convert.ToInt32(listPage);
            }
            Types = where;
            pageIndex = page;
            Limit = limit;

            classifyList = _classifyService.GetListAsync(m => !m.IsDel, m => m.FirstLetter, DbOrderEnum.Asc).Result.data;
            if (category!=null)
            {
                Classify = category;
                category = classifyList.FirstOrDefault(m => m.EnClassName == category)?.Guid;
            }
            var param = new PageParm() { guid = category,page = page, key = key,audit = 1,where = where,limit = limit };
            Ask = _askService.GetPageList(param).Result.data;
        }
    }
}