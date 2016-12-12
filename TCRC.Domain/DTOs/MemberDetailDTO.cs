using System;
using System.Collections.Generic;

namespace TCRC.Domain.DTOs
{
    public sealed class MemberDetailDTO
    {
        public bool HasPendingRenewal { get; set; }
        public int SotIdForRenewal { get; set; }
        public DateTime SotExpireDate { get; set; }
        public int TcrcIdForRenewal { get; set; }
        public DateTime TcrcExpireDate { get; set; }
        public bool HasMissingPayment { get; set; }
        public bool HasPendingEmergencyAssessments { get; set; }

        public IList<ClaimDTO> Claims { get; set; }
    }
}