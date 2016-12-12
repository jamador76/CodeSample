using System;

namespace TCRC.Models.Member
{
    public sealed class SellerOfTravelViewModel
    {
        public int SotOrgId { get; set; }
        public int SotId { get; set; }
        public int TcrcId { get; set; }
        public DateTime RegExpirationDate { get; set; }
        public string SotStatus { get; set; }
        public DateTime SotApprovedDate { get; set; }
    }
}