using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class QuestionAuditParam
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Text { get; set; }
    }
}
