using System;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models.Member
{
    public class BusinessContact
    {
        public int BusinessContactId { get; set; }

        public int TcrcId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }

        [Display(Name = "Check this box to receive renewal notices by mail instead of email")]
        public bool PrimaryContactMailRenewal { get; set; }

        [Display(Name = "Telephone")]
        [Required(ErrorMessage = "Telephone is required")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax")]
        public string FaxNumber { get; set; }

        [Display(Name = "First Name")]
        public string SecondaryFirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string SecondaryMiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string SecondaryLastName { get; set; }

        [Display(Name = "Title")]
        public string SecondaryTitle { get; set; }

        [Display(Name = "Email Address")]
        public string SecondaryEmailAddress { get; set; }

        [Display(Name = "Telephone")]
        public string SecondaryPhoneNumber { get; set; }

        [Display(Name = "Fax")]
        public string SecondaryFaxNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}