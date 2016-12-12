using System;
using System.Collections.Generic;

namespace TCRC.Models
{
    public class NavigationMenuItem
    {
        public string Text { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }

        public bool isActive { get; set; }

        public bool isHidden { get; set; }

        public IList<NavigationMenuItem> SubMenu { get; set; }
    }
}