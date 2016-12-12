using System;

namespace DAL.Repositories
{
    public sealed class SotOrgRepository : GenericRepository<SotOrg>
    {
        /// <summary>
        /// Sot org repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public SotOrgRepository(TCRCEntities context)
            : base(context) { }
    }
}