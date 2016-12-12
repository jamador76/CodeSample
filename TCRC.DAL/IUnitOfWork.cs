using System;
using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<Member> MemberRepository { get; }
        GenericRepository<Claim> ClaimRepository { get; }
        GenericRepository<ClaimStatusType> ClaimStatusTypeRepository { get; }
        GenericRepository<SotOrg> SotOrgRepository { get; }
        GenericRepository<SotStaging> SotStagingRepository { get; }
        GenericRepository<BusinessAddress> BusinessAddressRepository { get; }
        GenericRepository<Payment> PaymentRepository { get; }
        GenericRepository<UserProfile> UserProfileRepository { get; }
        void Save();
        void Dispose();
    }
}