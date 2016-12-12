using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using TCRC.Domain.DTOs;

namespace DAL.Repositories
{
    public sealed class MemberRepository : GenericRepository<Member>
    {
        /// <summary>
        /// Member repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public MemberRepository(TCRCEntities context)
            : base(context) { }

        /// <summary>
        /// Determines if member has pending emergency assessments
        /// </summary>
        /// <param name="tcrcId">The tcrc id</param>
        public bool HasPendingEmergencyAssessments(int tcrcId)
        {
            bool result = (from m in context.Members
                           join r in context.EmergencyAssessmentRecipients on m.TcrcId equals r.TcrcId
                           join a in context.EmergencyAssessments on r.EmergencyAssessmentId equals a.EmergencyAssessmentId
                           where m.TcrcId == tcrcId && !a.IsInactive && !r.IsPaid
                           select r).Any();
            return result;
        }

        /// <summary>
        /// Gets agency by city or zip code
        /// </summary>
        /// <param name="agency">The agency</param>
        /// <param name="address1">The address</param>
        /// <param name="city">The city</param>
        /// <param name="zipCode">The zip code</param>
        /// <param name="primaryContact">The primary contact</param>
        /// <param name="secondaryContact">The secondary contact</param>
        /// <param name="phone">The phone number</param>
        /// <returns>Returns a list of agencies</returns>
        public List<AgencyDTO> GetAgencyByCityOrZipCode(string agency, string address1, string city, string zipCode, string primaryContact, string secondaryContact, string phone)
        {
            var agencies = (from m in context.Members
                            join s in context.SotOrgs on m.TcrcId equals s.TcrcId
                            join a in context.BusinessAddresses on m.TcrcId equals a.TcrcId
                            join c in context.BusinessContacts on m.TcrcId equals c.TcrcId
                            where a.IsPoBox == false && m.ExpireDate >= DateTime.Now && s.RegExpirationDate >= DateTime.Now
                            && (m.LegalBusinessName.Contains(agency) || a.Address1.Contains(address1) || a.City == city || a.ZipCode == zipCode || 
                                c.FirstName.Contains(primaryContact) || c.MiddleName.Contains(primaryContact) || c.LastName.Contains(primaryContact) ||
                                c.SecondaryFirstName.Contains(secondaryContact) || c.SecondaryMiddleName.Contains(secondaryContact) || c.SecondaryLastName.Contains(secondaryContact) || a.PhoneNumber.Contains(phone))
                            select new AgencyDTO
                            {
                                Name = m.LegalBusinessName,
                                Address = a.Address1,
                                City = a.City,
                                State = a.State,
                                ZipCode = a.ZipCode,
                                PrimaryFirstName = c.FirstName,
                                PrimaryMiddleName = c.MiddleName,
                                PrimaryLastName = c.LastName,
                                SecondaryFirstName = c.SecondaryFirstName,
                                SecondaryMiddleName = c.SecondaryMiddleName,
                                SecondaryLastName = c.SecondaryLastName,
                                PhoneNumber = a.PhoneNumber
                            }).ToList();

            return agencies;
        }

        /// <summary>
        /// Gets member history by username
        /// </summary>
        /// <param name="userName">The username</param>
        public MemberHistoryDTO GetMemberHistoryByUserName(string userName)
        {
            var result = (from h in context.MemberHistories
                          join m in context.Members on h.TcrcId equals m.TcrcId
                          where m.UserName == userName
                          orderby h.CreateDate descending//, p.CreateDate descending
                          select new MemberHistoryDTO
                          {
                              MemberHistoryId = h.MemberHistoryId,
                              AssessmentFee = h.AssessmentFee,
                              LateFee = h.LateFee,
                              TotalFee = h.TotalFee,
                              MemberModeType = h.MemberModeType,
                          }).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Get members
        /// </summary>
        /// <param name="tcrcID">The tcrc id</param>
        /// <param name="sotID">The sot id</param>
        /// <param name="city">The city</param>
        /// <param name="zip">The zip code</param>
        /// <returns>Returns a list of members</returns>
        public IList<MemberLookupDTO> GetMembers(int? tcrcID, int? sotID, string city, string zip)
        {
            var members = (from m in context.Members
                           join s in context.SotOrgs on m.TcrcId equals s.TcrcId
                           join b in context.BusinessAddresses on m.TcrcId equals b.TcrcId
                           join c in context.BusinessContacts on m.TcrcId equals c.TcrcId
                           where b.IsPrimary == true && (m.TcrcId == tcrcID || s.SotId == sotID || b.City == city || b.ZipCode == zip)
                           select new MemberLookupDTO
                           {
                               TcrcID = m.TcrcId,
                               SotID = s.SotId,
                               LegalBusinessName = m.LegalBusinessName,
                               ArcIatan = m.ArcIatan,
                               Address1 = b.Address1,
                               Address2 = b.Address2,
                               City = b.City,
                               State = b.State,
                               ZipCode = b.ZipCode,
                               IsPrimary = b.IsPrimary,
                               PhoneNumber = b.PhoneNumber,
                               EmailAddress = c.EmailAddress,
                               NumberOfLocations = m.NumberOfLocations,
                               ExpireDate = m.ExpireDate,
                               RegExpirationDate = s.RegExpirationDate
                           }).ToList();
            return members;
        }

        /// <summary>
        /// Search for members
        /// </summary>
        /// <param name="tcrcID">The tcrc id</param>
        /// <param name="agency">The agency</param>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="isMissingPayment">Determines if mis</param>
        /// <returns>Returns list of members</returns>
        public IList<MemberDTO> SearchMembers(int? tcrcID, string agency, string firstName, string lastName, bool isMissingPayment)
        {
            //todo: find better way than using hardcoded values in place of enums
            //todo: also validate tcrc before calling this method
            var members = (from m in context.Members
                           let p = context.Payments.Where(x => x.PaymentForTypeId != 4
                               && x.PaymentTypeId != 1
                               && x.ReferenceId == m.TcrcId
                               && (x.CheckBouncedDate != null || x.CheckClearedDate == null))
                           join c in context.BusinessContacts on m.TcrcId equals c.TcrcId
                           where (tcrcID == 0 || m.TcrcId == tcrcID)
                               && (string.IsNullOrEmpty(agency) || m.LegalBusinessName.Contains(agency))
                               && (string.IsNullOrEmpty(firstName) || c.FirstName.Contains(firstName))
                               && (string.IsNullOrEmpty(lastName) || c.LastName.Contains(lastName))
                               && (!isMissingPayment || m.IsPendingPayment || m.IsPendingRenewal || p.Count() > 0)
                           orderby m.LegalBusinessName
                           select new MemberDTO
                           {
                               TcrcNumber = m.TcrcId,
                               FirstName = c.FirstName,
                               LastName = c.LastName,
                               AgencyName = m.LegalBusinessName,
                               TcrcExpirationDate = m.ExpireDate,
                               SotExpirationDate = m.SotOrgs.Count > 0 ? m.SotOrgs.OrderByDescending(d => d.RegExpirationDate).FirstOrDefault().RegExpirationDate != null ? m.SotOrgs.OrderByDescending(d => d.RegExpirationDate).FirstOrDefault().RegExpirationDate : null : null
                           }).ToList();
            
            return members;
        }

        /// <summary>
        /// Get member check payments history
        /// </summary>
        /// <param name="tcrcId">The tcrc id</param>
        /// <returns>Returns a list of members check histories</returns>
        public IList<MemberCheckHistoryDTO> GetMemberCheckHistories(int tcrcId)
        {
            var checkHistories = (from h in context.MemberHistories
                                  join p in context.Payments on h.MemberHistoryId equals p.ReferenceId into pmt
                                  from p in pmt.DefaultIfEmpty()
                                  where h.TcrcId == tcrcId
                                    && p != null
                                    && p.PaymentForTypeId != 3
                                    && p.PaymentForTypeId != 4
                                    && p.PaymentTypeId != 1
                                  orderby p.CreateDate descending
                                  select new MemberCheckHistoryDTO
                                  {
                                      CreateDate = p.CreateDate,
                                      CheckNumber = p.CheckNumber,
                                      Status = p.CheckBouncedDate != null ? "Check Bounced" : p.CheckClearedDate != null ? "Check Cleared" : "Check Received",
                                      Notes = p.Notes
                                  }).ToList();

            return checkHistories;
        }

        /// <summary>
        /// Update member entity
        /// </summary>
        /// <param name="entity">The member history</param>
        public override void Update(Member entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = context.Entry<Member>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = context.Set<Member>();
                Member attachedEntity = set.Local.SingleOrDefault(e => e.TcrcId == entity.TcrcId);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);

                    //update child objects
                    var addressesFromDb = attachedEntity.BusinessAddresses.ToList();

                    foreach (var addressFromDb in addressesFromDb)
                    {
                        //check if address was deleted
                        var address = entity.BusinessAddresses.SingleOrDefault(a => a.BusinessAddressId == addressFromDb.BusinessAddressId);

                        if (address != null)
                        {
                            context.Entry(addressFromDb).CurrentValues.SetValues(address);
                        }
                        //todo: verify if we need to add this else if a delete is required as part of an update
                        //else
                        //{
                        //    context.BusinessAddresses.Remove(addressFromDB);
                        //}
                    }

                    var businessContactsFromDb = attachedEntity.BusinessContacts.ToList();

                    foreach (var businessContactFromDb in businessContactsFromDb)
                    {
                        //check if contact was deleted
                        var businessContact = entity.BusinessContacts.SingleOrDefault(bc => bc.BusinessContactId == businessContactFromDb.BusinessContactId);

                        if (businessContact != null)
                        {
                            context.Entry(businessContactFromDb).CurrentValues.SetValues(businessContact);
                        }
                        //todo: verify if need to add this else if a delete is required as part of an update
                        //else
                        //{
                        //    context.BusinessContacts.Remove(businessContactFromDB);
                        //}
                    }

                    var businessDbaNamesFromDb = attachedEntity.BusinessDbaNames.ToList();

                    foreach (var businessDbaNameFromDb in businessDbaNamesFromDb)
                    {
                        //checif if dba was deleted
                        var businessDbaName = entity.BusinessDbaNames.SingleOrDefault(bd => bd.BusinessDbaNameId == businessDbaNameFromDb.BusinessDbaNameId);

                        if (businessDbaName != null)
                        {
                            context.Entry(businessDbaNameFromDb).CurrentValues.SetValues(businessDbaName);
                        }
                        //todo: verify if need to add this else if a delete is required as part of an update
                        //else
                        //{
                        //    context.BusinessDbaNames.Remove(businessDbaNameFromDb);
                        //}
                    }
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }
    }
}