using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TCRC.Domain.DTOs;

namespace TCRC.Models.Admin
{
    public class SearchClaimViewModel
    {
        [Display(Name = "TCRC Number")]
        public int? TCRCNumber { get; set; }

        [Display(Name = "Agency Name ")]
        public string AgencyName { get; set; }

        [Display(Name = "Claim Number")]
        public int? ClaimNumber { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Claimant Name (First or Last)")]
        public string ClaimantName { get; set; }

        [Display(Name = "Claim Date")]
        public DateTime? ClaimDate { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Claim Status")]
        public int? ClaimStatus { get; set; }

        public static IList<SelectListItem> ClaimStatuses
        {
            get
            {
                List<SelectListItem> claimsStatuses = new List<SelectListItem>();
                claimsStatuses.Add(new SelectListItem() { Text = "", Value = "" });
                claimsStatuses.Add(new SelectListItem() { Text = "Abandoned", Value = "2" });
                claimsStatuses.Add(new SelectListItem() { Text = "Appeal Denied", Value = "7" });
                claimsStatuses.Add(new SelectListItem() { Text = "Approved, Not Paid", Value = "4" });
                claimsStatuses.Add(new SelectListItem() { Text = "Approved, Paid", Value = "3" });
                claimsStatuses.Add(new SelectListItem() { Text = "Check Bounced", Value = "11" });
                claimsStatuses.Add(new SelectListItem() { Text = "Check Cleared", Value = "10" });
                claimsStatuses.Add(new SelectListItem() { Text = "Check Received", Value = "9" });
                claimsStatuses.Add(new SelectListItem() { Text = "Denied", Value = "5" });
                claimsStatuses.Add(new SelectListItem() { Text = "Fee Refunded", Value = "8" });
                claimsStatuses.Add(new SelectListItem() { Text = "In Appeal", Value = "6" });
                claimsStatuses.Add(new SelectListItem() { Text = "In Process", Value = "1" });
                claimsStatuses.Add(new SelectListItem() { Text = "In Process Admin", Value = "12" });

                return claimsStatuses;
            }
        }

        public IList<ClaimSearchDTO> Claims { get; set; }
    }
}