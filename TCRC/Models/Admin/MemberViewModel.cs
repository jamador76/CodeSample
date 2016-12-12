using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TCRC.Models.Member;

namespace TCRC.Models.Admin
{
    public sealed class MemberViewModel
    {
        public int TcrcNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgencyName { get; set; }
        public DateTime TcrcExpirationDate { get; set; }
        public DateTime? SotExpirationDate { get; set; }
    }
}