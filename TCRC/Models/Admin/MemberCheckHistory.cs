using System;

namespace TCRC.Models.Admin
{
    public sealed class MemberCheckHistory
    {
        public DateTime CreateDate { get; set; }
        public long? CheckNumber { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}