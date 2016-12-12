using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCRC.Models.Admin
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}