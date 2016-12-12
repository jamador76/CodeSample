using System;

namespace TCRC.Domain.DTOs
{
    public sealed class ClaimPaymentProofDTO
    {
        public int ClaimPaymentProofId { get; set; }
        public int ClaimId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentForm { get; set; }
        public bool ProofIncluded { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}