using System;

namespace DAL.Repositories
{
    public sealed class PaymentRepository : GenericRepository<Payment>
    {
        /// <summary>
        /// Payment repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public PaymentRepository(TCRCEntities context)
            : base(context) { }
    }
}
