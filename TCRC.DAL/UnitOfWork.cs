using System;
using System.Data.Entity.Validation;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Members
        private readonly TCRCEntities context;
        #endregion

        #region Methods
        /// <summary>
        /// Unit of work constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public UnitOfWork(TCRCEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Claim Repository
        /// </summary>
        public GenericRepository<Claim> ClaimRepository
        {
            get
            {
                return new ClaimRepository(context);
            }
        }

        /// <summary>
        /// Member repository
        /// </summary>
        public GenericRepository<Member> MemberRepository
        {
            get
            {
                return new MemberRepository(context);
            }
        }

        /// <summary>
        /// Business address repository
        /// </summary>
        public GenericRepository<BusinessAddress> BusinessAddressRepository
        {
            get
            {
                return new BusinessAddressRepository(context);
            }
        }

        /// <summary>
        /// Payment repository
        /// </summary>
        public GenericRepository<Payment> PaymentRepository
        {
            get
            {
                return new PaymentRepository(context);
            }
        }

        /// <summary>
        /// User profile repository
        /// </summary>
        public GenericRepository<UserProfile> UserProfileRepository
        {
            get
            {
                return new UserProfileRepository(context);
            }
        }

        /// <summary>
        /// Saves repository changes
        /// </summary>
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //todo: log error
                    //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        //todo: log error
                        //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        //    ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                //todo: log error
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}