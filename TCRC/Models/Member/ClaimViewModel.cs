using System;

namespace TCRC.Models.Member
{
    public sealed class ClaimViewModel
    {
        public int ClaimID { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal ClaimAmountTotal { get; set; }
        public string Status { get; set; }
    }
}