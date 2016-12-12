using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using TCRC.Domain.DTOs;

namespace DAL.Repositories
{
    public sealed class ClaimRepository : GenericRepository<Claim>
    {
        /// <summary>
        /// Claim repository constructor
        /// </summary>
        /// <param name="context">The database context</param>
        public ClaimRepository(TCRCEntities context)
            : base(context) { }

        /// <summary>
        /// Searches claims
        /// </summary>
        /// <param name="tcrcID">The TCRC id</param>
        /// <param name="claimID">The claim id</param>
        /// <param name="statusID">The status id</param>
        /// <param name="claimantName">The claimant name</param>
        /// <param name="city">The city</param>
        /// <param name="zip">The zip code</param>
        /// <param name="agencyName">The agency name</param>
        /// <param name="created">The created date</param>
        /// <param name="isAdmin">Determines if is admin</param>
        /// <returns>Returns a list of claims</returns>
        public IList<ClaimSearchDTO> SearchClaims(int? tcrcID, int? claimID, int? statusID, string claimantName, string city, string zip, string agencyName, DateTime? created, bool isAdmin)
        {
            //fixme: below uses an enum but was hardcoded just to get it working.  find a better solution.
            int[] status = { 7, 4, 3, 5 };

            var claims = (from c in context.Claims
                          where (tcrcID == null || c.TcrcId == tcrcID)
                            && (claimID == null || c.ClaimId == claimID)
                            && (statusID == null || c.ClaimStatusTypeId == statusID)
                            && (String.IsNullOrEmpty(claimantName) || c.FirstName.Contains(claimantName) || c.LastName.Contains(claimantName))
                            && (String.IsNullOrEmpty(city) || c.City.StartsWith(city))
                            && (String.IsNullOrEmpty(zip) || c.ZipCode.StartsWith(zip))
                            && (String.IsNullOrEmpty(agencyName) || c.BusinessAddress.AgencyName.Contains(agencyName))
                            && (created == null || (DateTime)c.CreateDate == created)
                            && (isAdmin || !status.Contains(c.ClaimStatusTypeId) || (status.Contains(c.ClaimStatusTypeId) && EntityFunctions.DiffDays(DateTime.Now, (DateTime)c.ModifyDate) < 60)) //(DateTime.Now - (DateTime)c.ModifyDate).Days < 60))
                          select new ClaimSearchDTO
                          {
                              ClaimNumber = c.ClaimId,
                              FirstName = c.FirstName,
                              LastName = c.LastName,
                              ClaimDate = c.CreateDate,
                              AgencyName = c.BusinessAddress.AgencyName,
                              Status = c.ClaimStatusType.ClaimStatusTypeName
                          }).ToList();

            return claims;
        }

        /// <summary>
        /// Updates the claim
        /// </summary>
        /// <param name="entity">The claim entity</param>
        public override void Update(Claim entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = context.Entry<Claim>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = context.Set<Claim>();
                Claim attachedEntity = set.Local.SingleOrDefault(e => e.ClaimId == entity.ClaimId);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);

                    //update child objects
                    var passengersFromDb = attachedEntity.ClaimPassengers.ToList();

                    foreach (var passengerFromDb in passengersFromDb)
                    {
                        //check if passenger was deleted
                        var passenger = entity.ClaimPassengers.SingleOrDefault(p => p.ClaimPassengerId == passengerFromDb.ClaimPassengerId);

                        if (passenger != null)
                        {
                            context.Entry(passengerFromDb).CurrentValues.SetValues(passenger);
                        }
                        //todo: verify if we need to add this else if a delete is required as part of an update
                        //else
                        //{
                        //    context.ClaimPassengers.Remove(passengerFromDb);
                        //}
                    }

                    var paymentsFromDb = attachedEntity.ClaimPaymentProofs.ToList();

                    foreach (var paymentFromDb in paymentsFromDb)
                    {
                        //check if payment was deleted
                        var payment = entity.ClaimPaymentProofs.SingleOrDefault(pr => pr.ClaimPaymentProofId == paymentFromDb.ClaimPaymentProofId);

                        if (payment != null)
                        {
                            context.Entry(paymentFromDb).CurrentValues.SetValues(payment);
                        }
                        //todo: verify if we need to add this else if a delete is required as part of an update
                        //else
                        //{
                        //    context.ClaimPaymentProofs.Remove(paymentFromDb);
                        //}
                    }

                    var documentsFromDb = attachedEntity.ClaimDocuments.ToList();

                    foreach (var documentFromDb in documentsFromDb)
                    {
                        //check if document was deleted
                        var document = entity.ClaimDocuments.SingleOrDefault(d => d.ClaimDocumentId == documentFromDb.ClaimDocumentId);

                        if (document != null)
                        {
                            context.Entry(documentFromDb).CurrentValues.SetValues(document);
                        }
                        //todo: verify if we need to add this else if a delete is required as part of an update
                        //else
                        //{
                        //    context.ClaimDocuments.Remove(documentFromDb);
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