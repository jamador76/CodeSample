using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TCRC.Domain.DTOs;

namespace TCRC.Models.Admin
{
    public class MemberSearchViewModel
    {
        public MemberSearchViewModel()
        {
            Members = new List<MemberViewModel>();
        }

        [Display(Name = "TCRC Number")]
        public int? TCRCNumber { get; set; }

        [Display(Name = "TCRC Agency Name ")]
        public string AgencyName { get; set; }

        [Display(Name = "Participant First Name")]
        public string ParticipantFirstName { get; set; }

        [Display(Name = "Participant Last Name")]
        public string ParticipantLastName { get; set; }

        [Display(Name = "Participants with Payment Process Incomplete")]
        public bool IsParticipantPaymentProcessIncomplete { get; set; }

        public IList<MemberViewModel> Members { get; set; }
    }
}