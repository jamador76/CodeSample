using System;

namespace TCRC.Domain.DTOs
{
    public sealed class PaymentDTO
    {
        public int MemberHistoryId { get; set; }
        public decimal AssessmentFee { get; set; }
        public decimal LateFee { get; set; }
        public decimal TotalAmountDue { get; set; }
        public string PaymentMethod { get; set; }
        public string CCFirstName { get; set; }
        public string CCLastName { get; set; }
        public string CCNumber { get; set; }
        public string CCExpirationDate { get; set; }
        public string CVVNumber { get; set; }
        public int? CheckNumber { get; set; }
        public decimal? CheckAmount { get; set; }
        public string Notes { get; set; }
    }
}