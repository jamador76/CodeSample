using System;
using System.Collections.Generic;
using TCRC.Domain.DTOs;

namespace TCRC.BusinessLogic.Services
{
    public interface IMemberService
    {
        MemberRegisterDTO GetMemberByID(string tcrcID);
        MemberRegisterDTO GetMemberByUserName(string userName);
        SellerOfTravelDTO GetSellerOfTravelByID(string sotID);
        MemberDetailDTO GetMemberDetailsByUserName(string userName);
        void CreateMember(MemberRegisterDTO model);
        void RenewMembership(MemberRegisterDTO model);
        IList<AgencyDTO> GetAgencyByCityOrZipCode(AgencySearchDTO model);
        PaymentDTO GetMemberHistory(string userName);
        void SubmitPayment(PaymentDTO model, string userName);
        IList<MemberLookupDTO> GetMembers(SOTReportDTO model);
        IList<MemberCheckHistoryDTO> GetMemberCheckHistories(int tcrcID);
        void ProcessMember(ProcessMemberDTO member, int userID);
        void UpdateMember(MemberRegisterDTO member, int userID);
    }
}