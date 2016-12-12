using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCRC.Models.Admin
{
    public class Member
    {
        public int TcrcID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Agency { get; set; }
        public DateTime TcrcExpiration { get; set; }
        public DateTime SotExpiration { get; set; }
    }
}