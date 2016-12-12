using System;
using System.Collections.Generic;

namespace TCRC.Domain.DTOs
{
    public sealed class MemberRegisterDTO
    {
        public int TcrcID { get; set; }
        public IList<MemberHistoryDTO> MemberHistories { get; set; }
        public IList<MemberNoteDTO> MemberNotes { get; set; }
        public int? TCRCNumber { get; set; }
        public int? SOTNumber { get; set; }
        public IList<BusinessContactDTO> BusinessContacts { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string LegalBusinessName { get; set; }
        public DateTime LegalBusinessStartDate { get; set; }
        public string ArcIatan { get; set; }
        public IList<BusinessDbaNameDTO> BusinessDbaNames { get; set; }
        public IList<BusinessAddressDTO> BusinessAddresses { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal LateFeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool HasAcceptedTerms { get; set; }
        public string MemberSearchResults { get; set; }
        public DateTime? SotRenewalDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}