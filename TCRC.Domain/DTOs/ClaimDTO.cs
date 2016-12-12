using System;

namespace TCRC.Domain.DTOs
{
    public sealed class ClaimDTO
    {
        public int ClaimID { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal ClaimAmountTotal { get; set; }
        public string Status { get; set; }
    }
}