using System;

namespace TCRC.Domain.DTOs
{
    public sealed class ClaimPassengerDTO
    {
        public int ClaimPassengerId { get; set; }
        public int ClaimId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
