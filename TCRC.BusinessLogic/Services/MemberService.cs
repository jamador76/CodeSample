using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using AutoMapper;
using DAL;
using DAL.Repositories;
using TCRC.BusinessLogic.Enums;
using TCRC.Domain.DTOs;

namespace TCRC.BusinessLogic.Services
{
    public sealed class MemberService : IMemberService
    {
        #region Members
        private readonly IUnitOfWork unitOfWork;
        private readonly MemberRepository memberRepository;
        #endregion

        #region Methods
        /// <summary>
        /// Member service constructor
        /// </summary>
        /// <param name="unitOfWork">The unit of work</param>
        public MemberService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            memberRepository = unitOfWork.MemberRepository as MemberRepository;
        }

        /// <summary>
        /// Gets a member by id
        /// </summary>
        /// <param name="tcrcID">The tcrc id</param>
        /// <returns>Returns a registered member</returns>
        public MemberRegisterDTO GetMemberByID(string tcrcID)
        {
            if (!String.IsNullOrEmpty(tcrcID))
            {
                int tcrcNum = int.Parse(tcrcID);
                Member member = memberRepository.Get(m => m.TcrcId.Equals(tcrcNum)).SingleOrDefault();
                var registerViewModel = Mapper.Map<Member, MemberRegisterDTO>(member);

                return registerViewModel;
            }
            return null;
        }

        /// <summary>
        /// Gets a member by username
        /// </summary>
        /// <param name="userName">The member username</param>
        /// <returns>Returns a registered member</returns>
        public MemberRegisterDTO GetMemberByUserName(string userName)
        {
            if (!String.IsNullOrEmpty(userName))
            {
                Member member = memberRepository.Get(m => m.UserName.Equals(userName)).SingleOrDefault();
                var registerViewModel = Mapper.Map<Member, MemberRegisterDTO>(member);

                if (registerViewModel != null)
                {
                    registerViewModel.SotRenewalDate = member.SotOrgs.OrderByDescending(s => s.RegExpirationDate).FirstOrDefault().RegExpirationDate;
                }

                return registerViewModel;
            }
            return null;
        }

        /// <summary>
        /// Gets member details by username
        /// </summary>
        /// <param name="userName">The username</param>
        /// <returns>Returns member details</returns>
        public MemberDetailDTO GetMemberDetailsByUserName(string userName)
        {
            if (!String.IsNullOrEmpty(userName))
            {
                Member member = memberRepository.Get(m => m.UserName.Equals(userName)).SingleOrDefault();
                MemberDetailDTO memberDetail = new MemberDetailDTO();

                if (member != null)
                {
                    SotOrg sot = unitOfWork.SotOrgRepository.Get(s => s.TcrcId == member.TcrcId).OrderByDescending(x => x.RegExpirationDate).FirstOrDefault();

                    DateTime sotExpireDate = DateTime.MinValue;
                    DateTime tcrcExpireDate = member.ExpireDate;

                    if (sot != null)
                    {
                        sotExpireDate = (DateTime)sot.RegExpirationDate;
                        memberDetail.SotIdForRenewal = sot.SotId;
                        memberDetail.SotExpireDate = sotExpireDate;
                    }

                    //todo: review this logic for pending renewal
                    memberDetail.HasPendingRenewal = ((sotExpireDate > DateTime.MinValue && (sotExpireDate - DateTime.Today).Days <= 75)
                        || (tcrcExpireDate > DateTime.MinValue && (tcrcExpireDate - DateTime.Today).Days <= 75));

                    memberDetail.HasMissingPayment = member.IsPendingPayment;
                    memberDetail.HasPendingEmergencyAssessments = memberRepository.HasPendingEmergencyAssessments(member.TcrcId);

                    memberDetail.TcrcIdForRenewal = member.TcrcId;
                    memberDetail.TcrcExpireDate = tcrcExpireDate;

                    if (member.Claims.Count > 0)
                    {
                        memberDetail.Claims = new List<ClaimDTO>();

                        foreach (var claim in member.Claims)
                        {
                            memberDetail.Claims.Add(new ClaimDTO
                            {
                                ClaimID = claim.ClaimId,
                                CreateDate = claim.CreateDate,
                                ClaimAmountTotal = claim.ClaimAmountTotal,
                                Status = claim.ClaimStatusType.ClaimStatusTypeName
                            });
                        }
                    }
                }
                return memberDetail;
            }
            return null;
        }

        /// <summary>
        /// Gets a seller of travel by id
        /// </summary>
        /// <param name="sotID">The sot id</param>
        /// <returns>Returns a seller of travel</returns>
        public SellerOfTravelDTO GetSellerOfTravelByID(string sotID)
        {
            if (!String.IsNullOrEmpty(sotID))
            {
                int sotNum = int.Parse(sotID);
                SotOrg sot = unitOfWork.SotOrgRepository.Get(s => s.SotId.Equals(sotNum)).SingleOrDefault();
                var sotViewModel = Mapper.Map<SotOrg, SellerOfTravelDTO>(sot);

                return sotViewModel;
            }
            return null;
        }

        /// <summary>
        /// Create a member
        /// </summary>
        /// <param name="model">The member model</param>
        public void CreateMember(MemberRegisterDTO model)
        {
            // DBA's are not required so we need to set the object to null so that it won't try to save to the database
            // this will also handle cases where users could add multiple dba names but leave one or more empty
            var itemsToRemove = model.BusinessDbaNames.Where(d => String.IsNullOrEmpty(d.DbaName)).ToList();

            foreach (var item in itemsToRemove)
            {
                model.BusinessDbaNames.Remove(item);
            }

            var member = Mapper.Map<MemberRegisterDTO, Member>(model);

            TimeSpan duration = new TimeSpan(365, 0, 0, 0);
            DateTime expireDate = DateTime.Now.Add(duration);
            member.ExpireDate = expireDate;

            MemberHistory memberHistory = new MemberHistory();
            memberHistory.PaidDate = DateTime.Now;
            memberHistory.ExpireDate = expireDate;
            memberHistory.AssessmentFee = model.SubtotalAmount;
            memberHistory.LateFee = model.LateFeeAmount;
            memberHistory.TotalFee = model.TotalAmount;
            memberHistory.MemberModeType = MemberModeType.New;
            memberHistory.CreateDate = DateTime.Now;

            member.MemberHistories.Add(memberHistory);

            memberRepository.Insert(member);
            unitOfWork.Save();
        }

        /// <summary>
        /// Renew a membership
        /// </summary>
        /// <param name="model">The member registration model</param>
        public void RenewMembership(MemberRegisterDTO model)
        {
            // DBA's are not required so we need to set the object to null so that it won't try to save to the database
            // this will also handle cases where users could add multiple dba names but leave one or more empty
            var itemsToRemove = model.BusinessDbaNames.Where(d => String.IsNullOrEmpty(d.DbaName)).ToList();

            foreach (var item in itemsToRemove)
            {
                model.BusinessDbaNames.Remove(item);
            }

            var member = Mapper.Map<MemberRegisterDTO, Member>(model);
            member.IsPendingPayment = true;

            DAL.UserProfile userProfile = unitOfWork.UserProfileRepository.Get(m => m.UserName == model.UserName).SingleOrDefault();
            userProfile.FirstName = model.BusinessContacts[0].FirstName;
            userProfile.LastName = model.BusinessContacts[0].LastName;
            userProfile.Email = model.BusinessContacts[0].EmailAddress;
            unitOfWork.UserProfileRepository.Update(userProfile);

            TimeSpan duration = new TimeSpan(365, 0, 0, 0);
            DateTime expireDate = DateTime.Now.Add(duration);
            member.ExpireDate = expireDate;

            MemberHistory memberHistory = new MemberHistory();
            memberHistory.PaidDate = DateTime.Now;
            memberHistory.AssessmentFee = model.SubtotalAmount;
            memberHistory.LateFee = model.LateFeeAmount;  //todo: current system adds "+ emergencyAssessmentFee" which doesn't seem to actually be implemented
            memberHistory.TotalFee = model.TotalAmount;
            memberHistory.ExpireDate = expireDate;
            memberHistory.MemberModeType = MemberModeType.Renewal;
            memberHistory.CreateDate = DateTime.Now;

            if (memberHistory.TotalFee > 0)
            {
                member.MemberHistories.Add(memberHistory);
            }
            else
            {
                //todo: send back error
            }

            memberRepository.Update(member);
            unitOfWork.Save();
        }

        /// <summary>
        /// Get agency by city or zip code
        /// </summary>
        /// <param name="agencySearch">The agency to search for</param>
        /// <returns></returns>
        public IList<AgencyDTO> GetAgencyByCityOrZipCode(AgencySearchDTO agencySearch)
        {
            var agencies = memberRepository.GetAgencyByCityOrZipCode(agencySearch.AgencyName, agencySearch.Address1, agencySearch.City, agencySearch.ZipCode, agencySearch.PrimaryContact, agencySearch.SecondaryContact, agencySearch.Phone);

            return agencies;
        }

        /// <summary>
        /// Gets member payment history
        /// </summary>
        /// <param name="userName">The username</param>
        /// <returns>Returns payment history</returns>
        public PaymentDTO GetMemberHistory(string userName)
        {
            MemberHistoryDTO memberHistory = memberRepository.GetMemberHistoryByUserName(userName);
            PaymentDTO payment = new PaymentDTO();

            if (memberHistory != null)
            {
                payment.MemberHistoryId = memberHistory.MemberHistoryId;
                payment.AssessmentFee = memberHistory.AssessmentFee;
                payment.LateFee = memberHistory.LateFee;
                payment.TotalAmountDue = memberHistory.TotalFee;
            }

            return payment;
        }

        /// <summary>
        /// Gets member check histories
        /// </summary>
        /// <param name="tcrcID">The tcrc id</param>
        /// <returns>Returns list of member check histories</returns>
        public IList<MemberCheckHistoryDTO> GetMemberCheckHistories(int tcrcID)
        {
            List<MemberCheckHistoryDTO> checkHistories = memberRepository.GetMemberCheckHistories(tcrcID).ToList();
            return checkHistories;
        }

        /// <summary>
        /// Gets members
        /// </summary>
        /// <param name="report">The sot report</param>
        /// <returns>Returns a list of members</returns>
        public IList<MemberLookupDTO> GetMembers(SOTReportDTO report)
        {
            var members = memberRepository.GetMembers(report.TcrcID, report.SotID, report.City, report.ZipCode).ToList();
            return members;
        }

        /// <summary>
        /// Processes a member
        /// </summary>
        /// <param name="model">The member model</param>
        /// <param name="userID">The user id</param>
        public void ProcessMember(ProcessMemberDTO model, int userID)
        {
            var member = memberRepository.GetByID(model.TcrcId);

            member.MemberNotes.Add(new DAL.MemberNote
            {
                TcrcId = model.TcrcId,
                Notes = model.Notes,
                CreateDate = DateTime.Now,
                CreateById = userID
            });

            unitOfWork.MemberRepository.Update(member);
            unitOfWork.Save();
        }

        /// <summary>
        /// Update a member
        /// </summary>
        /// <param name="model">The member model</param>
        /// <param name="userID">The user id</param>
        public void UpdateMember(MemberRegisterDTO model, int userID)
        {
            var tempMember = unitOfWork.MemberRepository.GetByID(model.TcrcID);
            var histories = tempMember.MemberHistories;
            var notes = tempMember.MemberNotes;
            
            var member = Mapper.Map<MemberRegisterDTO, DAL.Member>(model);
            //we need to preserve these because of the way entity framework works which will overwrite them and they'll be lost otherwise
            member.MemberHistories = histories;
            member.MemberNotes = notes;

            unitOfWork.MemberRepository.Update(member);
            unitOfWork.Save();
        }

        /// <summary>
        /// Submits a payment
        /// </summary>
        /// <param name="model">The payment model</param>
        /// <param name="userName">The username</param>
        public void SubmitPayment(PaymentDTO model, string userName)
        {
            MemberHistoryDTO memberHistory = memberRepository.GetMemberHistoryByUserName(userName);

            Payment payment = new Payment();
            payment.ReferenceId = memberHistory.MemberHistoryId;
            payment.SubtotalAmount = model.AssessmentFee;
            payment.LateFeeAmount = model.LateFee;
            payment.TotalAmount = model.TotalAmountDue;
            payment.CreateDate = DateTime.Now;

            //check if admin
            payment.Notes = model.Notes;

            switch (memberHistory.MemberModeType)
            {
                case MemberModeType.New:
                    payment.PaymentForTypeId = 1;
                    break;
                case MemberModeType.Renewal:
                    payment.PaymentForTypeId = 2;
                    break;
                case MemberModeType.MemberProfile:
                    break;
                case MemberModeType.AdminEdit:
                    break;
                default:
                    //todo: determine default case
                    break;
            }

            if (model.PaymentMethod.ToLower().Equals("check"))
            {
                //todo: review setting payment type id. old code does this: payment.PaymentTypeId = (int)(isAdmin ? PaymentType.CheckReceived : PaymentType.CheckEntered);
                payment.PaymentTypeId = (int)TCRC.BusinessLogic.Enums.PaymentType.CheckEntered;
                payment.CheckNumber = model.CheckNumber;
                payment.CheckAmount = model.CheckAmount;
                payment.CheckDate = DateTime.Now;
            }
            else if (model.PaymentMethod.ToLower().Equals("credit"))
            {
                payment.PaymentTypeId = (int)TCRC.BusinessLogic.Enums.PaymentType.CreditCard;

                //todo: review this logic for cc payment. this was brought in from old system which might have a bug.
                bool approved = false;
                string authCode = string.Empty;

                try
                {
                    string authorizeLoginId = ConfigurationSettings.AppSettings["AuthorizeNetLoginId"];
                    string authorizeTransactionKey = ConfigurationSettings.AppSettings["AuthorizeNetTransactionKey"];
                    string responseCode;

                    String postUrl = "https://secure.authorize.net/gateway/transact.dll";

                    Hashtable postValues = new Hashtable();

                    postValues.Add("x_login", authorizeLoginId);
                    postValues.Add("x_tran_key", authorizeTransactionKey);
                    postValues.Add("x_version", "3.0");

                    postValues.Add("x_delim_data", "TRUE");
                    postValues.Add("x_delim_char", '|');
                    postValues.Add("x_relay_response", "FALSE");

                    postValues.Add("x_type", "AUTH_CAPTURE");
                    postValues.Add("x_method", "CC");
                    //postValues.Add("x_cust_id", "");  This could be TcrcId or ClaimId for audit purposes.  A way to uniquely ID the customer.
                    postValues.Add("x_first_name", model.CCFirstName);
                    postValues.Add("x_last_name", model.CCLastName);
                    postValues.Add("x_card_num", model.CCNumber);
                    string[] dateParts = model.CCExpirationDate.Split('/');
                    postValues.Add("x_exp_date", dateParts[0] + dateParts[1].Substring(2, 2));
                    postValues.Add("x_card_code", model.CVVNumber);

                    postValues.Add("x_amount", model.TotalAmountDue);
                    postValues.Add("x_test_request", "FALSE");

                    string postString = String.Empty;
                    foreach (DictionaryEntry field in postValues)
                    {
                        postString += field.Key + "=" + field.Value + "&";
                    }
                    postString = postString.TrimEnd('&');

                    // HttpWebRequest: communicate with Authorize.net
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
                    request.Method = "POST";
                    request.ContentLength = postString.Length;
                    request.ContentType = "application/x-www-form-urlencoded";
                    // Request
                    StreamWriter writer = null;
                    writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(postString);
                    writer.Close();

                    // Response
                    string postResponse;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
                    {
                        postResponse = responseStream.ReadToEnd();
                        responseStream.Close();
                    }

                    string[] responseArray = postResponse.Split('|');

                    // Response Array: See Fields in Payment Gateway Response for order: http://developer.authorize.net/guides/AIM/
                    responseCode = responseArray[0]; //Response Code: 1=Approved, 2=Declined, 3=Error, 4=Held for Review
                    authCode = responseArray[4]; // Authorization Code: 6 characters

                    approved = (responseCode == "1") ? true : false;

                    if (responseCode != "1")
                    {
                        //todo: do we need to insert to Audits table?

                        //UserInfo user = UserController.GetCurrentUserInfo();
                        //Context.Audits.InsertOnSubmit(new Audit { FieldName = "PaymentGatewayResponse", NewValue = postResponse, TableName = "PaymentGateway", Type = 'I', UpdateDate = DateTime.Now, UserName = (user != null) ? user.Username : "Gilardi_User" });
                        //Context.SubmitChanges();
                    }

                    if (approved)
                    {
                        payment.CCAuthNumber = authCode;
                        unitOfWork.PaymentRepository.Insert(payment);
                        unitOfWork.Save();
                    }
                }
                catch (Exception e)
                {
                    //todo: do something with exception
                }
            }

            unitOfWork.PaymentRepository.Insert(payment);
            unitOfWork.Save();
        }
        #endregion
    }
}