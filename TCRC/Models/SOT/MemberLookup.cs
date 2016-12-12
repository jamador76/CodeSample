using System;

namespace TCRC.Models.SOT
{
    public sealed class MemberLookup
    {
        public int TcrcID { get; set; }
        public int SotID { get; set; }
        public string LegalBusinessName { get; set; }
        public string ArcIatan { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsPrimary { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? NumberOfLocations { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime? RegExpirationDate { get; set; }
    }
}