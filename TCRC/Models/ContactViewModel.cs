using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCRC.Models
{
    public class ContactViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}