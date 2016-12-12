using System;

namespace TCRC.Models.FileClaim
{
    public class ClaimHistory
    {
        public Int16 ClaimStatusTypeId { get; set; }
        public string Notes { get; set; }
        public string EmailText { get; set; }
        public Int64 ApprovedCheckNumber { get; set; }
        public decimal ApprovedCheckAmount { get; set; }
        public DateTime ApprovedCheckDate { get; set; }
        public decimal FeeRefundAmount { get; set; }
        public Int64 ReceivedCheckNumber { get; set; }
        public decimal ReceivedCheckAmount { get; set; }
        public DateTime ReceivedCheckDate { get; set; }
        public DateTime ReceivedCheckClearedDate { get; set; }
        public DateTime ReceivedCheckBouncedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
    }
}