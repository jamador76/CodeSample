using System;

namespace TCRC.Domain.DTOs
{
    public sealed class SellerOfTravelDTO
    {
        public int SotOrgId { get; set; }
        public int SotId { get; set; }
        public int TcrcId { get; set; }
        public DateTime RegExpirationDate { get; set; }
        public string SotStatus { get; set; }
        public DateTime SotApprovedDate { get; set; }
    }
}