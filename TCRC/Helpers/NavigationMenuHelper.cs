using System;
using System.Collections.Generic;
using System.Linq;
using TCRC.Models;

namespace TCRC.Helpers
{
    public class NavigationMenuHelper
    {
        private static IList<NavigationMenuItem> navigationMenu = new List<NavigationMenuItem>
        {
            new NavigationMenuItem { Text = "Home", Action = "Index", Controller = "Home" },
            new NavigationMenuItem { Text = "Become A Participant", Action = "Register", Controller = "Member" },
          
            new NavigationMenuItem { Text = "File A Claim", Action = "ClaimForm", Controller = "FileClaim", SubMenu = new List<NavigationMenuItem>
                {
                    new NavigationMenuItem { Text = "Download Claim Packet", Action = "DownloadClaimPacket", Controller = "FileClaim" }
                }
            },
            new NavigationMenuItem { Text = "About Us", Action = "About", Controller = "Home", SubMenu = new List<NavigationMenuItem>
                {
                    new NavigationMenuItem { Text = "Bylaws", Action = "Bylaws", Controller = "Home"},
                    new NavigationMenuItem { Text = "Statute", Action = "Statute", Controller = "Home"},
                    new NavigationMenuItem { Text = "Consumer Questions", Action = "ConsumerQuestions", Controller = "Home"},
                    new NavigationMenuItem { Text = "Yearly Statistics", Action = "YearlyStatistics", Controller = "Home"}
                }
            },
            new NavigationMenuItem { Text = "Search For Agency", Action = "AgencySearch", Controller = "Home" },
            new NavigationMenuItem { Text = "Contact Us", Action = "Contact", Controller = "Home" },
           
            new NavigationMenuItem { Text = "Log In", Action = "Login", Controller = "Account" },
            new NavigationMenuItem { Text = "Participant", Action = "Details", Controller = "Member", isActive = true,  SubMenu = new List<NavigationMenuItem>
                {
                    new NavigationMenuItem { Text = "My Profile", Action = "MyProfile", Controller = "Member" },
                    new NavigationMenuItem { Text = "Change Password", Action = "ChangePassword", Controller = "Member" },
                    new NavigationMenuItem { Text = "Renew Membership", Action = "RenewMembership", Controller = "Member" },
                }
            },
            new NavigationMenuItem { Text = "Participant Admin", Action = "", Controller = "", isActive = true,  SubMenu = new List<NavigationMenuItem>
            {                  
                    new NavigationMenuItem { Text = "Search Members", Action = "SearchMembers", Controller = "Admin" },
                    new NavigationMenuItem { Text = "Emergency Assessment", Action = "EmergencyAssessment", Controller = "Admin" }
                }
            },
            new NavigationMenuItem { Text = "Seller Of Travel", Action = "Index", Controller = "SOT" },
            new NavigationMenuItem { Text = "Claims Admin", Action = "", Controller = "", SubMenu = new List<NavigationMenuItem>           
                {
                    new NavigationMenuItem { Text = "Add New Claim", Action = "AddNewClaim", Controller = "Admin" },
                    new NavigationMenuItem { Text = "Search Claim", Action = "SearchClaim", Controller = "Admin" }
                }
            },
             new NavigationMenuItem { Text = "Admin", Action = "", Controller = "", SubMenu = new List<NavigationMenuItem>            
                {
                    new NavigationMenuItem { Text = "Reports", Action = "Reports", Controller = "Admin" },
                    new NavigationMenuItem { Text = "SOT Import", Action = "ImportSot", Controller = "Admin" },
                    new NavigationMenuItem { Text = "Users", Action = "Users", Controller = "Admin" },
                    new NavigationMenuItem { Text = "Attorney General", Action = "Index", Controller = "AttorneyGeneral" }
                }
            }
        };

        #region Methods
        /// <summary>
        /// Gets the navigation menu
        /// </summary>
        /// <returns>Returns the navigation menu</returns>
        public static IList<NavigationMenuItem> GetNavigationMenu()
        {
            return navigationMenu;
        }

        /// <summary>
        /// Displays hidden menus
        /// </summary>
        public static void DisplayHiddenMenus()
        {
            //todo: check user's role and then display the appropriate menus
            navigationMenu.Single(m => m.Text == "Admin").isHidden = false;
            navigationMenu.Single(m => m.Text == "Participant").isHidden = false;

          navigationMenu.Single(m => m.Text == "Log In").isHidden = true;
           
        }

        /// <summary>
        /// Hides menus
        /// </summary>
        public static void HideMenus()
        {
            navigationMenu.Single(m => m.Text == "Admin").isHidden = true;
            navigationMenu.Single(m => m.Text == "Participant").isHidden = true;
            navigationMenu.Single(m => m.Text == "Log In").isHidden = false;
        }

        /// <summary>
        /// Sets active navigation menu item
        /// </summary>
        /// <param name="activeAction">The active action</param>
        /// <param name="activeController">The active controller</param>
        public static void SetActiveNavigationMenuItem(string activeAction, string activeController)
        {
            foreach (var item in navigationMenu)
            {
                if (item.Action.Equals(activeAction) && item.Controller.Equals(activeController))
                {
                    item.isActive = true;
                }
                else
                {
                    item.isActive = false;
                }

                if (item.isActive == false && item.SubMenu != null)
                {
                    foreach (var submenuItem in item.SubMenu)
                    {
                        if (submenuItem.Action.Equals(activeAction) && submenuItem.Controller.Equals(activeController))
                        {
                            item.isActive = true;
                        }
                    }
                }
            }
        }
        #endregion
    }
}