using System;

namespace TCRC.Domain.DTOs
{
    public sealed class BusinessAddressDTO
    {
        public int BusinessAddressId { get; set; }
        public int TcrcId { get; set; }
        public string AgencyName { get; set; }
        public DateTime BusinessStartDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Zip4 { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public bool HasNoCustomers { get; set; }
        public bool IsPOBox { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsMailing { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}