using System;

namespace DAL.Repositories
{
    public sealed class UserProfileRepository : GenericRepository<UserProfile>
    {
        /// <summary>
        /// User profile repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public UserProfileRepository(TCRCEntities context)
            : base(context) { }
    }
}
