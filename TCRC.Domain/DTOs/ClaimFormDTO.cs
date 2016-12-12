using System;
using System.Collections.Generic;

namespace TCRC.Domain.DTOs
{
    public sealed class ClaimFormDTO
    {
        public int ClaimId { get; set; }
        public IList<ClaimHistoryDTO> ClaimHistories { get; set; }
        public byte ClaimStatusTypeId { get; set; }
        public int TcrcId { get; set; }
        public int BusinessAddressId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public IList<ClaimPassengerDTO> ClaimPassengers { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipCode4 { get; set; }
        public string PhoneNumberPrimary { get; set; }
        public string PhoneNumberSecondary { get; set; }
        public string EmailAddress { get; set; }
        public IList<ClaimPaymentProofDTO> ClaimPaymentProofs { get; set; }
        public string ScheduledServicesPurchased { get; set; }
        public DateTime ScheduledDepartureDate { get; set; }
        public DateTime ScheduledReturnDate { get; set; }
        public string SpecificDetails { get; set; }
        public string TCRCNumber { get; set; }
        public string SOTNumber { get; set; }
        public string AgencyName { get; set; }
        public string AgencyCity { get; set; }
        public string SellerID { get; set; }
        public decimal ClaimAmountTotal { get; set; }
        public string ClaimAmountBasis { get; set; }
        public IList<ClaimDocumentDTO> ClaimDocuments { get; set; }
        public Guid? UploadKey { get; set; }
        public string MissingDocumentDetails { get; set; }
        public bool HasPurchasedInsurance { get; set; }
        public DateTime? InsurancePurchaseDate { get; set; }
        public decimal? InsurancePurchaseAmount { get; set; }
        public string InsurancePurchasedFrom { get; set; }
        public string Insurer { get; set; }
        public bool HasOtherReimbursement { get; set; }
        public string ReimbursementSource { get; set; }
        public string ReimbursementDetails { get; set; }
        public bool HasAcceptedTerms { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Error { get; set; }
    }
}