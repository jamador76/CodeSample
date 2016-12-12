using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using AutoMapper;
using DAL;
using DAL.Repositories;
using TCRC.BusinessLogic.Enums;
using TCRC.Domain.DTOs;

namespace TCRC.BusinessLogic.Services
{
    public sealed class ClaimService : IClaimService
    {
        #region Members
        private readonly IUnitOfWork unitOfWork;
        private readonly BusinessAddressRepository businessAddressRepository;
        private readonly ClaimRepository claimRespository;
        private readonly ClaimStatusTypeRepository claimStatusTypeRepository;
        #endregion

        #region Methods
        /// <summary>
        /// Claim service constructor
        /// </summary>
        /// <param name="unitOfWork">The unit of work</param>
        public ClaimService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            businessAddressRepository = unitOfWork.BusinessAddressRepository as BusinessAddressRepository;
            claimRespository = unitOfWork.ClaimRepository as ClaimRepository;
            claimStatusTypeRepository = unitOfWork.ClaimStatusTypeRepository as ClaimStatusTypeRepository;
        }

        /// <summary>
        /// Gets business addresses for a claim
        /// </summary>
        /// <param name="tcrcId">The tcrc id</param>
        /// <param name="sotId">The sot id</param>
        /// <param name="businessAddressId">The business address id</param>
        /// <param name="agencyName">The agency name</param>
        /// <param name="city">The city</param>
        /// <param name="claimDateStr">The claim date</param>
        /// <param name="scheduledReturnDateStr">The scheduled return date</param>
        /// <returns>Returns a list of business addresses for claim</returns>
        public IList<BusinessAddressDTO> GetBusinessAddressesForClaim(int? tcrcId, int? sotId, int businessAddressId, string agencyName, string city, string claimDateStr, string scheduledReturnDateStr)
        {
            DateTime claimDate = DateTime.Parse(claimDateStr);
            DateTime scheduledReturnDate = DateTime.Parse(scheduledReturnDateStr);

            if (tcrcId == null)
            {
                tcrcId = 0;
            }

            if (sotId == null)
            {
                sotId = 0;
            }

            List<BusinessAddressDTO> businessAddresses = new List<BusinessAddressDTO>();

            //todo: limit number of addresses to 200
            if ((tcrcId > 0 || sotId > 0 || businessAddressId > 0 || !String.IsNullOrEmpty(agencyName) || !String.IsNullOrEmpty(city)) && claimDate > DateTime.MinValue && scheduledReturnDate > DateTime.MinValue)
            {
                businessAddresses = businessAddressRepository.GetBusinessAddressesForClaim(tcrcId, sotId, 0, agencyName, city, claimDate, scheduledReturnDate);
            }

            return businessAddresses;
        }

        /// <summary>
        /// Inserts a claim
        /// </summary>
        /// <param name="model">The claim</param>
        public void InsertClaim(ClaimFormDTO model)
        {
            try
            {
                //passengers are not required so we need to set the object to null so that it won't incorrectly save to the database
                var passengersToRemove = model.ClaimPassengers.Where(p => String.IsNullOrEmpty(p.FirstName) || String.IsNullOrEmpty(p.LastName)).ToList();

                foreach (var passenger in passengersToRemove)
                {
                    model.ClaimPassengers.Remove(passenger);
                }

                var claim = Mapper.Map<ClaimFormDTO, DAL.Claim>(model);
                claim.ClaimStatusTypeId = (byte)StatusType.InProcess;

                var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads/" + model.UploadKey);
                if (Directory.Exists(uploadPath))
                {
                    foreach (var fileName in Directory.GetFiles(uploadPath))
                    {
                        //todo: determine exactly what we should be saving for file url
                        claim.ClaimCollaterals.Add(new DAL.ClaimCollateral { FileName = Path.GetFileName(fileName), FileUrl = Path.GetFullPath(fileName), CreateDate = DateTime.Now });
                    }
                }

                //todo: CreateById should be able to accept nulls in the database in the case of a new claim instead of hardcoding -1
                claim.ClaimHistories.Add(new DAL.ClaimHistory { ClaimStatusTypeId = claim.ClaimStatusTypeId, Notes = "New Claim Submitted", CreateDate = DateTime.Now, CreateById = -1 });

                var businessAddress = businessAddressRepository.GetByID(model.BusinessAddressId);
                claim.TcrcId = businessAddress.TcrcId;

                unitOfWork.ClaimRepository.Insert(claim);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                //todo: do something with this exception
            }
        }

        /// <summary>
        /// Updates a claim
        /// </summary>
        /// <param name="model">The claim</param>
        public void UpdateClaim(ClaimFormDTO model)
        {
            var claim = Mapper.Map<ClaimFormDTO, DAL.Claim>(model);

            unitOfWork.ClaimRepository.Update(claim);
            unitOfWork.Save();
        }

        /// <summary>
        /// Gets a claim by id
        /// </summary>
        /// <param name="claimID">The claim id</param>
        /// <returns></returns>
        public ClaimFormDTO GetClaimByID(int claimID)
        {
            DAL.Claim claimData = unitOfWork.ClaimRepository.GetByID(claimID);
            ClaimFormDTO claim = Mapper.Map<DAL.Claim, ClaimFormDTO>(claimData);

            return claim;
        }

        /// <summary>
        /// Searchs for a claim
        /// </summary>
        /// <param name="searchClaim">The claim to search</param>
        /// <returns></returns>
        public IList<ClaimSearchDTO> SearchClaims(SearchClaimDTO searchClaim)
        {
            //todo: isAdmin is hardcoded to true, this should be done through authentication instead using annotations on this method
            var claims = claimRespository.SearchClaims(searchClaim.TCRCNumber, searchClaim.ClaimNumber, searchClaim.ClaimStatus, searchClaim.ClaimantName, searchClaim.City,
                searchClaim.ZipCode, searchClaim.AgencyName, searchClaim.ClaimDate, true);
            
            return claims;
        }

        /// <summary>
        /// Gets a claim status type
        /// </summary>
        /// <param name="claimId">The claim id</param>
        /// <param name="claimStatusTypeId">The claim status id</param>
        /// <param name="claimant">The claimant</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="state">The state</param>
        /// <param name="zip">The zip code</param>
        /// <returns>Returns a claim status type</returns>
        public ClaimStatusTypeDTO GetClaimStatusType(int claimId, int claimStatusTypeId, string claimant, string address, string city, string state, string zip)
        {
            ClaimStatusType claimStatusType = claimStatusTypeRepository.GetByID(claimStatusTypeId);
            
            StringBuilder emailText = new StringBuilder(claimStatusType.EmailTemplate);
            emailText.Replace("{date}", DateTime.Now.ToShortDateString());
            emailText.Replace("{claimnumber}", claimId.ToString());
            emailText.Replace("{claimant}", claimant);
            emailText.Replace("{address}", address);
            emailText.Replace("{city}", city);
            emailText.Replace("{state}", state);
            emailText.Replace("{zip}", zip);

            claimStatusType.EmailTemplate = emailText.ToString();

            return Mapper.Map<ClaimStatusType, ClaimStatusTypeDTO>(claimStatusType);
        }

        /// <summary>
        /// Updates a claim
        /// </summary>
        /// <param name="model">The claim to update</param>
        /// <param name="userID">The user id</param>
        public void UpdateClaim(ClaimFormDTO model, int userID)
        {
            //get the claim histories and collaterals so they don't get overwritten
            var tempClaim = unitOfWork.ClaimRepository.GetByID(model.ClaimId);
            var claimHistories = tempClaim.ClaimHistories;
            var claimCollaterals = tempClaim.ClaimCollaterals;
            var createDate = tempClaim.CreateDate;

            var claim = Mapper.Map<ClaimFormDTO, DAL.Claim>(model);
            //need to reassign the claim histories and collaterals or they will be lost
            claim.ClaimHistories = claimHistories;
            claim.ClaimCollaterals = claimCollaterals;

            unitOfWork.ClaimRepository.Update(claim);
            unitOfWork.Save();
        }

        /// <summary>
        /// Process a claim
        /// </summary>
        /// <param name="model">The claim</param>
        /// <param name="userID">The user id</param>
        public void ProcessClaim(ProcessClaimDTO model, int userID)
        {
            var payment = new Payment
            {
                ReferenceId = model.ClaimID,
                CheckAmount = model.ReceivedCheckAmount,
                CheckBouncedDate = model.ReceivedCheckBouncedDate,
                CheckClearedDate = model.ReceivedCheckClearedDate,
                CheckDate = model.ReceivedCheckDate,
                CheckNumber = model.ReceivedCheckNumber,
                SubtotalAmount = model.ReceivedCheckAmount ?? 0,
                TotalAmount = model.ReceivedCheckAmount ?? 0,
                PaymentForTypeId = (int)TCRC.BusinessLogic.Enums.PaymentForType.Claim,
                PaymentTypeId = (int)TCRC.BusinessLogic.Enums.PaymentType.CheckReceived,
                CreateDate = DateTime.Now,
                CreateById = userID
            };

            unitOfWork.PaymentRepository.Insert(payment);

            var claim = unitOfWork.ClaimRepository.GetByID(model.ClaimID);

            claim.ClaimHistories.Add(new DAL.ClaimHistory
            {
                ClaimId = model.ClaimID,
                ClaimStatusTypeId = model.ClaimStatusTypeId,
                Notes = model.Notes,
                EmailText = model.EmailText,
                ApprovedCheckAmount = model.ApprovedCheckAmount,
                ApprovedCheckNumber = model.ApprovedCheckNumber,
                ApprovedCheckDate = model.ApprovedCheckDate,
                FeeRefundAmount = model.FeeRefundAmount,
                ReceivedCheckAmount = model.ReceivedCheckAmount,
                ReceivedCheckBouncedDate = model.ReceivedCheckBouncedDate,
                ReceivedCheckClearedDate = model.ReceivedCheckClearedDate,
                ReceivedCheckDate = model.ReceivedCheckDate,
                ReceivedCheckNumber = model.ReceivedCheckNumber,
                CreateDate = DateTime.Now,
                CreateById = userID
            });

            unitOfWork.ClaimRepository.Update(claim);
            unitOfWork.Save();
        }
        #endregion
    }
}