using System;

namespace TCRC.Domain.DTOs
{
    public sealed class MemberDTO
    {
        public int TcrcNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgencyName { get; set; }
        public DateTime TcrcExpirationDate { get; set; }
        public DateTime? SotExpirationDate { get; set; }
    }
}
