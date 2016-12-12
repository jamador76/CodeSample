using System;

namespace TCRC.Domain.DTOs
{
    public sealed class ClaimSearchDTO
    {
        public int ClaimNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ClaimDate { get; set; }
        public string AgencyName { get; set; }
        public string Status { get; set; }
    }
}