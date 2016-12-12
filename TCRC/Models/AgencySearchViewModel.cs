using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models
{
    public class AgencySearchViewModel
    {
        public AgencySearchViewModel()
        {
            Agencies = new List<AgencyViewModel>();
        }

        [Display(Name = "Agency Name")]
        public string AgencyName { get; set; }

        [Display(Name = "Street Address")]
        public string Address1 { get; set; }

        [Display(Name = "Primary Contact Name")]
        public string PrimaryContact { get; set; }

        [Display(Name = "Secondary Contact Name")]
        public string SecondaryContact { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        public IList<AgencyViewModel> Agencies { get; set; }
    }
}