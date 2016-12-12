using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TCRC.Models.SOT
{
    public class SOTReportViewModel
    {
        public static IList<SelectListItem> Reports
        {
            get
            {
                List<SelectListItem> reports = new List<SelectListItem>();
                reports.Add(new SelectListItem() { Text = "TCRC Member Lookup", Value = "1" });
                reports.Add(new SelectListItem() { Text = "TCRC Business Name Lookup", Value = "2" });
                reports.Add(new SelectListItem() { Text = "Business Address Lookup", Value = "3" });
                reports.Add(new SelectListItem() { Text = "Claim Lookup", Value = "4" });

                return reports;
            }
        }

        [Display(Name = "Select Report")]
        public int ReportID { get; set; }

        [Display(Name = "TCRC ID")]
        public int? TcrcID { get; set; }

        [Display(Name = "SOT ID")]
        public int? SotID { get; set; }

        public string City { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "")]
        public string BusinessName { get; set; }

        public string Address { get; set; }

        public IList<MemberLookup> Members { get; set; }
    }
}