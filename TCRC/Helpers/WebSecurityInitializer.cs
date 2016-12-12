using System;
//using System.Collections;
using WebMatrix.WebData;

namespace TCRC.Helpers
{
    public sealed class WebSecurityInitializer
    {
        #region Members
        private WebSecurityInitializer() { }

        public static readonly WebSecurityInitializer Instance = new WebSecurityInitializer();
        private bool isNotInit = true;
        private readonly object SyncRoot = new object();
        #endregion

        #region Methods
        /// <summary>
        /// Ensure membership database has been intialized
        /// </summary>
        public void EnsureInitialize()
        {
            if (isNotInit)
            {
                lock (this.SyncRoot)
                {
                    if (isNotInit)
                    {
                        isNotInit = false;
                        WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    }
                }
            }
        }
        #endregion
    }
}