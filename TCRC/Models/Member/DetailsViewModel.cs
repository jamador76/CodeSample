using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCRC.Models.Member
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
            Claims = new List<ClaimViewModel>();
        }

        public bool HasPendingRenewal { get; set; }
        public int SotIdForRenewal { get; set; }
        public DateTime SotExpireDate { get; set; }
        public int TcrcIdForRenewal { get; set; }
        public DateTime TcrcExpireDate { get; set; }
        public bool HasMissingPayment { get; set; }
        public bool HasPendingEmergencyAssessments { get; set; }

        public IList<ClaimViewModel> Claims { get; set; }
    }
}