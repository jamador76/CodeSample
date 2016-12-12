using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using AutoMapper;
using DAL;
using DAL.Repositories;
using TCRC.Domain.DTOs;

namespace TCRC.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        #region Members
        private readonly IUnitOfWork unitOfWork;
        private readonly SotStagingRepository sotStagingRepository;
        private readonly MemberRepository memberRepository;
        #endregion

        #region Methods
        /// <summary>
        /// Admin service constructor
        /// </summary>
        /// <param name="unitOfWork">The unit of work</param>
        public AdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            sotStagingRepository = unitOfWork.SotStagingRepository as SotStagingRepository;
            memberRepository = unitOfWork.MemberRepository as MemberRepository;
        }

        /// <summary>
        /// Import sot records
        /// </summary>
        /// <param name="filePath">The file path</param>
        public void ImportSots(string filePath)
        {
            sotStagingRepository.TruncateSotStaging();

            DateTime dresult;
            Int64 iresult;

            var sots = from line in File.ReadAllLines(filePath).Skip(1)
                       let record = line.Split(',')
                       select new SotStaging
                       {
                           SotApprovalDate = DateTime.TryParse(record[0], out dresult) ? dresult : Convert.ToDateTime("1/1/1753"),
                           SotExpirationDate = DateTime.TryParse(record[1], out dresult) ? dresult : Convert.ToDateTime("1/1/1753"),
                           SotId = Int64.TryParse(record[2], out iresult) ? iresult : 0,
                           IsInitialFiling = (record[4].ToLower() == "y") ? true : false,
                           SotStatus = record[5],
                           TcrcId = Int64.TryParse(record[6], out iresult) ? iresult : 0
                       };

            sotStagingRepository.BulkInsert(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, "SotStaging", sots.ToList());

            sotStagingRepository.ImportSot();
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>Returns list of user profiles</returns>
        public IList<UserProfileDTO> GetUsers()
        {
            var users = unitOfWork.UserProfileRepository.Get().ToList();
            List<UserProfileDTO> userProfiles = Mapper.Map<List<DAL.UserProfile>, List<UserProfileDTO>>(users);

            return userProfiles;
        }

        /// <summary>
        /// Search members
        /// </summary>
        /// <param name="memberSearch">The members to search for</param>
        /// <returns>Returns list of members</returns>
        public IList<MemberDTO> SearchMembers(MemberSearchDTO memberSearch)
        {
            int? tcrcID = 0;
            if (memberSearch.TcrcNumber != null)
            {
                tcrcID = memberSearch.TcrcNumber;
            }

            List<MemberDTO> members = memberRepository.SearchMembers(tcrcID, memberSearch.AgencyName, memberSearch.FirstName, memberSearch.LastName, memberSearch.IsParticipantPaymentProcessIncomplete).ToList();

            return members;
        }
        #endregion
    }
}