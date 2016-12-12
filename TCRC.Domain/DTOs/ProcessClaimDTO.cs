using System;

namespace TCRC.Domain.DTOs
{
    public sealed class ProcessClaimDTO
    {
        public byte ClaimStatusTypeId { get; set; }
        public int ClaimID { get; set; }
        public string EmailText { get; set; }
        public bool DoNotSendLetter { get; set; }
        public int? ApprovedCheckNumber { get; set; }
        public decimal? ApprovedCheckAmount { get; set; }
        public DateTime? ApprovedCheckDate { get; set; }
        public decimal? FeeRefundAmount { get; set; }
        public int? ReceivedCheckNumber { get; set; }
        public decimal? ReceivedCheckAmount { get; set; }
        public DateTime? ReceivedCheckDate { get; set; }
        public DateTime? ReceivedCheckClearedDate { get; set; }
        public DateTime? ReceivedCheckBouncedDate { get; set; }
        public string Notes { get; set; }
    }
}