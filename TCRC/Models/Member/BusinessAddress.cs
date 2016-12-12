using System;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models.Member
{
    public class BusinessAddress
    {
        public int BusinessAddressId { get; set; }

        public int TcrcId { get; set; }

        [Display(Name = "Agency Name")]
        [Required(ErrorMessage = "Agency Name is required")]
        public string AgencyName { get; set; }

        [Display(Name = "Date Started at this Location")]
        [Required(ErrorMessage = "Date Started is required")]
        public DateTime BusinessStartDate { get; set; }

        [Display(Name = "Address 1")]
        [Required(ErrorMessage = "Address is required")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }

        public string Zip4 { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax Number")]
        public string Fax { get; set; }

        [Display(Name = "I do not do business with customers at this location")]
        public bool HasNoCustomers { get; set; }

        [Display(Name = "Address is P.O. Box")]
        public bool IsPOBox { get; set; }

        [Display(Name = "This is my primary business location")]
        [Required(ErrorMessage = "Primary business location is required")]
        public bool IsPrimary { get; set; }

        [Display(Name = "This is my business mailing address")]
        [Required(ErrorMessage = "Business mailing address is required")]
        public bool IsMailing { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}