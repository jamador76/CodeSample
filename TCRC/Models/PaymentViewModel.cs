using System;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models
{
    public class PaymentViewModel
    {
        public int MemberHistoryId { get; set; }

        [Display(Name = "Assessment Fee")]
        public decimal AssessmentFee { get; set; }

        [Display(Name = "Late Fee")]
        public decimal LateFee { get; set; }

        [Display(Name = "Total Amount Due")]
        public decimal TotalAmountDue { get; set; }

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "First Name")]
        public string CCFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string CCLastName { get; set; }

        [Display(Name = "Credit Card Number")]
        public string CCNumber { get; set; }

        [Display(Name = "Expiration Date")]
        public string CCExpirationDate { get; set; }

        [Display(Name = "CVV Number")]
        public string CVVNumber { get; set; }

        [Display(Name = "Check Number")]
        public int? CheckNumber { get; set; }

        [Display(Name = "Check Amount")]
        public decimal? CheckAmount { get; set; }

        public string Notes { get; set; }
    }
}