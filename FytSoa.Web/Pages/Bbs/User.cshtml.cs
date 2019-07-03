using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Core.Model.Member;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.Bbs
{
    public class UserModel : PageModel
    {
        private readonly IMemberService _memberService;
        private readonly IMember_GroupService _groupService;
        private readonly IBbs_QuestionsService _questionsService;
        private readonly IBbs_AnswerService _answerService;
        public UserModel(IMemberService memberService
        , IMember_GroupService groupService
        , IBbs_QuestionsService questionsService
        , IBbs_AnswerService answerService)
        {
            _memberService = memberService;
            _groupService = groupService;
            _questionsService = questionsService;
            _answerService = answerService;
        }

        public Member User { get; set; }

        public string GroupName { get; set; }

        public int QuestionCount { get; set; } = 0;

        public int AnswerCount { get; set; } = 0;

        public Page<Bbs_Questions> QuestionList { get; set; }

        public Page<AnswerDto> AnswerList { get; set; }

        /// <summary>
        /// 分页当前码
        /// </summary>
        public int pageIndex { get; set; } = 0;

        /// <summary>
        /// 类型
        /// </summary>
        public string Types { get; set; }

        public void OnGet(string id,string type="")
        {
            int page = 1;
            var listPage = Request.Query["page"];
            if (!string.IsNullOrEmpty(listPage))
            {
                page = Convert.ToInt32(listPage);
            }
            pageIndex = page;

            User = _memberService.GetModelAsync(m => m.Guid == id).Result.data;
            var groupGuid = User.Grade;
            var groupModel = _groupService.GetModelAsync(m => m.Guid == groupGuid).Result.data;
            GroupName = groupModel.Name;

            QuestionCount = _questionsService.CountAsync(m => m.UserGuid==id).Result.data.Count;

            AnswerCount= _answerService.CountAsync(m => m.UserGuid == id).Result.data.Count;

            Types = type;
            if (type=="")
            {
                QuestionList = _questionsService.GetPagesAsync(new PageParm() {page=page }, m => m.UserGuid == id,m=>m.AddTime,DbOrderEnum.Desc).Result.data;
            }
            else
            {
                AnswerList= _answerService.GetUserCenterAnswer(new PageParm() { page = page,guid = id}).Result.data;
            }
        }
    }
}