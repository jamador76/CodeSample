using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TCRC.Models.FileClaim
{
    public class ClaimFormViewModel
    {
        public ClaimFormViewModel()
        {
            ClaimPassengers = new List<ClaimPassenger>();
            ClaimPaymentProofs = new List<ClaimPaymentProof>();
            ClaimDocuments = new List<ClaimDocument>();
            ClaimHistories = new List<ClaimHistory>();
        }

        public int ClaimID { get; set; }

        public IList<ClaimHistory> ClaimHistories { get; set; }

        public byte ClaimStatusTypeId { get; set; }

        public int TcrcId { get; set; }

        [Required(ErrorMessage = "Travel Seller is required")]
        public int BusinessAddressId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public IList<ClaimPassenger> ClaimPassengers { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }

        public string ZipCode4 { get; set; }

        [Display(Name = "Daytime Telephone")]
        [Required(ErrorMessage = "Daytime Phone is required")]
        public string PhoneNumberPrimary { get; set; }

        [Display(Name = "Evening Phone")]
        public string PhoneNumberSecondary { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid Email Address!")]
        [Required(ErrorMessage = "Email Address is required")]
        [StringLength(254)]
        public string EmailAddress { get; set; }

        public static IList<SelectListItem> PaymentForms
        {
            get
            {
                IList<SelectListItem> paymentForms = new List<SelectListItem>();

                paymentForms.Add(new SelectListItem() { Text = "Cash", Value = "Cash" });
                paymentForms.Add(new SelectListItem() { Text = "Check", Value = "Check" });
                paymentForms.Add(new SelectListItem() { Text = "Credit Card", Value = "Credit Card" });
                paymentForms.Add(new SelectListItem() { Text = "Money Order", Value = "Money Order" });
                return paymentForms;
            }
        }

        public IList<ClaimPaymentProof> ClaimPaymentProofs { get; set; }

        [Required(ErrorMessage = "Description of services is required")]
        public string ScheduledServicesPurchased { get; set; }

        [Display(Name = "Scheduled Departure Date")]
        [Required(ErrorMessage = "Scheduled date of departure is required")]
        public DateTime ScheduledDepartureDate { get; set; }

        [Display(Name = "Scheduled Return Date")]
        [Required(ErrorMessage = "Scheduled return date is required")]
        public DateTime ScheduledReturnDate { get; set; }

        public string SpecificDetails { get; set; }

        [Display(Name = "TCRC Number")]
        public string TCRCNumber { get; set; }

        [Display(Name = "SOT Number")]
        public string SOTNumber { get; set; }

        [Display(Name = "Agency Name")]
        public string AgencyName { get; set; }

        [Display(Name = "City")]
        public string AgencyCity { get; set; }

        public string SellerID { get; set; }

        [Display(Name = "Total Amount of Claim")]
        [Required(ErrorMessage = "Total amount of claim is required")]
        public decimal ClaimAmountTotal { get; set; }

        [Display(Name = "Explanation of basis for claim")]
        [Required(ErrorMessage = "Explanation of basis for claim is required")]
        public string ClaimAmountBasis { get; set; }

        public IList<ClaimDocument> ClaimDocuments { get; set; }

        public Guid? UploadKey { get; set; }

        [Display(Name = "If any of the required documents are not provided, please state why below.")]
        public string MissingDocumentDetails { get; set; }

        public bool HasPurchasedInsurance { get; set; }

        [Display(Name = "Date purchased")]
        public DateTime? InsurancePurchaseDate { get; set; }

        [Display(Name = "Amount Paid")]
        public decimal? InsurancePurchaseAmount { get; set; }

        [Display(Name = "Purchased from (name and address)")]
        public string InsurancePurchasedFrom { get; set; }

        [Display(Name = "Insurer (name and address)")]
        public string Insurer { get; set; }

        public bool HasOtherReimbursement { get; set; }

        [Display(Name = "Source (name, address, phone, file or contract number)")]
        public string ReimbursementSource { get; set; }

        [Display(Name = "Details")]
        public string ReimbursementDetails { get; set; }

        public bool HasAcceptedTerms { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Error { get; set; }
    }
}