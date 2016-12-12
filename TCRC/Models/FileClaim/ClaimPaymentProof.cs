using System;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models.FileClaim
{
    public class ClaimPaymentProof
    {
        public int ClaimPaymentProofId { get; set; }

        public int ClaimId { get; set; }

        [Display(Name = "Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Amount")]
        public decimal PaymentAmount { get; set; }

        [Display(Name = "Form")]
        public string PaymentForm { get; set; }

        [Display(Name = "Proof will be included")]
        public bool ProofIncluded { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}