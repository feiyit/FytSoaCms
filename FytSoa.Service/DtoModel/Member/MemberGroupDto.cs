using FytSoa.Core.Model.Member;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    public class MemberGroupDto
    {
        public Member member { get; set; }

        public List<Member_Group> group { get; set; }
    }
}
