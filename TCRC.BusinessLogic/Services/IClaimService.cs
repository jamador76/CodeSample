using System;
using System.Collections.Generic;
using TCRC.Domain.DTOs;

namespace TCRC.BusinessLogic.Services
{
    public interface IClaimService
    {
        List<BusinessAddressDTO> GetBusinessAddressesForClaim(int? tcrcId, int? sotId, int businessAddressId, string agencyName, string city, string claimDateStr, string scheduledReturnDateStr);
        void InsertClaim(ClaimFormDTO model);
        void UpdateClaim(ClaimFormDTO model);
        ClaimFormDTO GetClaimByID(int claimID);
        IList<ClaimSearchDTO> SearchClaims(SearchClaimDTO model);
        void UpdateClaim(ClaimFormDTO model, int UserID);
        void ProcessClaim(ProcessClaimDTO model, int UserID);
        ClaimStatusTypeDTO GetClaimStatusType(int claimId, int claimStatusTypeId, string claimant, string address, string city, string state, string zip);
    }
}