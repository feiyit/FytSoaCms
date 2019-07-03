using System;
using System.Collections.Generic;
using System.Text;
using FytSoa.Core.Model.Bbs;
using FytSoa.Core.Model.Member;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 前端右侧数据汇总
    /// </summary>
    public class PageRightDto
    {
        /// <summary>
        /// 问题总数
        /// </summary>
        public int QuestionCount { get; set; } = 0;

        /// <summary>
        /// 会员总数
        /// </summary>
        public int UserCount { get; set; } = 0;

        /// <summary>
        /// 热门标签
        /// </summary>
        public List<TagsCount> TagList { get; set; }

        /// <summary>
        /// 热门问题
        /// </summary>
        public List<Bbs_Questions> RedQuestionList { get; set; }

        /// <summary>
        /// 推荐专家
        /// </summary>
        public List<MemberQuestion> ExpertList { get; set; }
    }

    /// <summary>
    /// 常用标签
    /// </summary>
    public class TagsCount
    {
        public string TagName { get; set; }

        public string EnTagName { get; set; }

        public int TagCount { get; set; }
    }

    /// <summary>
    /// 热门话题
    /// </summary>
    public class MemberQuestion
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPic { get; set; }

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 回答总数
        /// </summary>
        public int AnswerCount { get; set; }

        /// <summary>
        /// 采纳总数
        /// </summary>
        public int AdoptCount { get; set; }
    }
}
