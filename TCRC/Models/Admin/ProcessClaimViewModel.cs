using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TCRC.Models.FileClaim;

namespace TCRC.Models.Admin
{
    public class ProcessClaimViewModel : ClaimFormViewModel
    {
        public static IList<SelectListItem> ClaimStatusTypes
        {
            get
            {
                IList<SelectListItem> statusTypes = new List<SelectListItem>();
                statusTypes.Add(new SelectListItem() { Text = "", Value = "", Selected = true });
                statusTypes.Add(new SelectListItem() { Text = "Abandoned", Value = "2" });
                statusTypes.Add(new SelectListItem() { Text = "Appeal Denied", Value = "7" });
                statusTypes.Add(new SelectListItem() { Text = "Approved, Not Paid", Value = "4" });
                statusTypes.Add(new SelectListItem() { Text = "Approved, Paid", Value = "3" });
                statusTypes.Add(new SelectListItem() { Text = "Check Bounced", Value = "11" });
                statusTypes.Add(new SelectListItem() { Text = "Check Cleared", Value = "10" });
                statusTypes.Add(new SelectListItem() { Text = "Check Received", Value = "9" });
                statusTypes.Add(new SelectListItem() { Text = "Denied", Value = "5" });
                statusTypes.Add(new SelectListItem() { Text = "Fee Refunded", Value = "8" });
                statusTypes.Add(new SelectListItem() { Text = "In Appeal", Value = "6" });
                statusTypes.Add(new SelectListItem() { Text = "In Process", Value = "1" });
                statusTypes.Add(new SelectListItem() { Text = "In Process Admin", Value = "12" });

                return statusTypes;
            }
        }

        [Display(Name = "Claim Status Letter")]
        public string EmailText { get; set; }

        [Display(Name = "Do Not Send Letter")]
        public bool DoNotSendLetter { get; set; }

        [Display(Name = "Claim Check Number")]
        public int? ApprovedCheckNumber { get; set; }

        [Display(Name = "Claim Check Amount")]
        public decimal? ApprovedCheckAmount { get; set; }

        [Display(Name = "Claim Check Date")]
        public DateTime? ApprovedCheckDate { get; set; }

        [Display(Name = "Fee Refund Amount")]
        public decimal? FeeRefundAmount { get; set; }

        [Display(Name = "Check Number")]
        public int? ReceivedCheckNumber { get; set; }

        [Display(Name = "Check Amount")]
        public decimal? ReceivedCheckAmount { get; set; }

        [Display(Name = "Check Date")]
        public DateTime? ReceivedCheckDate { get; set; }

        [Display(Name = "Check Cleared Date")]
        public DateTime? ReceivedCheckClearedDate { get; set; }

        [Display(Name = "Check Bounced Date")]
        public DateTime? ReceivedCheckBouncedDate { get; set; }

        public string Notes { get; set; }
    }
}