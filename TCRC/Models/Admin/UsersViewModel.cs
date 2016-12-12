using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCRC.Models.Admin
{
    public class UsersViewModel
    {
        public UsersViewModel()
        {
            Users = new List<UserProfileViewModel>();
        }

        public IList<UserProfileViewModel> Users { get; set; }
    }
}