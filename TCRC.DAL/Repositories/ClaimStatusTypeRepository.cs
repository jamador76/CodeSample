using System;

namespace DAL.Repositories
{
    public sealed class ClaimStatusTypeRepository : GenericRepository<ClaimStatusType>
    {
        /// <summary>
        /// Claim status type repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public ClaimStatusTypeRepository(TCRCEntities context)
            : base(context) { }
    }
}