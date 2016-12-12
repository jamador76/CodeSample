using System;

namespace TCRC.Domain.DTOs
{
    public sealed class MemberSearchDTO
    {
        public int TcrcNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgencyName { get; set; }
        public bool IsParticipantPaymentProcessIncomplete { get; set; }
    }
}