using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models.Member
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            BusinessContacts = new List<BusinessContact>();
            BusinessDbaNames = new List<BusinessDbaName>();
            BusinessAddresses = new List<BusinessAddress>();
            MemberHistories = new List<MemberHistory>();
            MemberNotes = new List<MemberNote>();
        }

        public int TcrcID { get; set; }

        public IList<MemberHistory> MemberHistories { get; set; }

        public IList<MemberNote> MemberNotes { get; set; }

        [Display(Name = "TCRC Control#")]
        public int? TCRCNumber { get; set; }

        [Display(Name = "SOT ID")]
        public int? SOTNumber { get; set; }

        public IList<BusinessContact> BusinessContacts { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Password must match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Legal Name")]
        [Required(ErrorMessage = "Legal Name is required")]
        public string LegalBusinessName { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime LegalBusinessStartDate { get; set; }

        [Display(Name = "ARC/IATAN Number")]
        public string ArcIatan { get; set; }
        
        public IList<BusinessDbaName> BusinessDbaNames { get; set; }

        public IList<BusinessAddress> BusinessAddresses { get; set; }

        [Display(Name = "Assessment Fee")]
        public decimal SubtotalAmount { get; set; }

        [Display(Name = "Late Fee")]
        public decimal LateFeeAmount { get; set; }

        [Display(Name = "Total Amount Due")]
        public decimal TotalAmount { get; set; }

        public bool HasAcceptedTerms { get; set; }

        public string MemberSearchResults { get; set; }

        public DateTime? SotRenewalDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}