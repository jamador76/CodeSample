using System;

namespace TCRC.Domain.DTOs
{
    public sealed class MemberHistoryDTO
    {
        public int MemberHistoryId { get; set; }
        public int TcrcID { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal AssessmentFee { get; set; }
        public decimal LateFee { get; set; }
        public decimal TotalFee { get; set; }
        public DateTime CreateDate { get; set; }
        public string MemberModeType { get; set; }
        public int CreateById { get; set; }
    }
}