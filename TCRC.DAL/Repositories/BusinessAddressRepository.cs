using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using TCRC.Domain.DTOs;

namespace DAL.Repositories
{
    public sealed class BusinessAddressRepository : GenericRepository<BusinessAddress>
    {
        /// <summary>
        /// Business address repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public BusinessAddressRepository(TCRCEntities context)
            : base(context) { }

        /// <summary>
        /// Gets business address for claim
        /// </summary>
        /// <param name="tcrcId">The TCRC id</param>
        /// <param name="sotId">The seller of travel id</param>
        /// <param name="businessAddressId">The business address id</param>
        /// <param name="agencyName">The agency name</param>
        /// <param name="city">The city</param>
        /// <param name="claimDate">The claim date</param>
        /// <param name="scheduledReturnDate">The scheduled return date</param>
        /// <returns>Returns a list of business addresses</returns>
        public IList<BusinessAddressDTO> GetBusinessAddressesForClaim(int? tcrcId, int? sotId, int businessAddressId, string agencyName, string city, DateTime claimDate, DateTime scheduledReturnDate)
        {
            var addresses = (from b in context.BusinessAddresses
                             where (tcrcId == 0 || b.TcrcId == tcrcId)
                             && (sotId == 0 || b.Member.SotOrgs.Any(x => x.SotId == sotId))
                             && (businessAddressId == 0 || b.BusinessAddressId == businessAddressId)
                             && (String.IsNullOrEmpty(agencyName) || b.AgencyName.Contains(agencyName))
                             && (String.IsNullOrEmpty(city) || b.City.StartsWith(city))
                             && !b.HasNoCustomers
                             && b.BusinessStartDate <= claimDate
                             && (!b.IsInactive || (b.IsInactive && claimDate <= b.InactiveDate))
                             && b.Member.MemberHistories.Any(h => SqlDateTime.MinValue.Value < h.PaidDate
                                 && h.PaidDate <= scheduledReturnDate
                                 && claimDate <= h.ExpireDate)
                             && b.Member.SotOrgs.Any(s => s.TcrcId == b.Member.TcrcId
                                 && s.SotApprovedDate != null
                                 && s.RegExpirationDate != null
                                 && SqlDateTime.MinValue.Value < s.SotApprovedDate
                                 && ((DateTime)s.SotApprovedDate) <= scheduledReturnDate
                                 && claimDate <= s.RegExpirationDate)
                             orderby b.AgencyName
                             select new BusinessAddressDTO
                             {
                                 BusinessAddressId = b.BusinessAddressId,
                                 AgencyName = b.AgencyName,
                                 Address1 = b.Address1,
                                 PhoneNumber = b.PhoneNumber
                             }).ToList();
            return addresses;
        }
    }
}