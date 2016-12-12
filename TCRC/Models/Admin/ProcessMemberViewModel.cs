using System;
using System.Collections.Generic;
using TCRC.Models.Member;

namespace TCRC.Models.Admin
{
    public class ProcessMemberViewModel : RegisterViewModel
    {
        public ProcessMemberViewModel()
        {
            MemberCheckHistories = new List<MemberCheckHistory>();
        }

        public IList<MemberCheckHistory> MemberCheckHistories { get; set; }
        public string Notes { get; set; }
    }
}