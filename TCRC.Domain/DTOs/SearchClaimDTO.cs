using System;

namespace TCRC.Domain.DTOs
{
    public sealed class SearchClaimDTO
    {
        public int? TCRCNumber { get; set; }
        public string AgencyName { get; set; }
        public int? ClaimNumber { get; set; }
        public string ZipCode { get; set; }
        public string ClaimantName { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string City { get; set; }
        public int? ClaimStatus { get; set; }
    }
}