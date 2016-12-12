using System;

namespace TCRC.Domain.DTOs
{
    public sealed class AgencySearchDTO
    {
        public string AgencyName { get; set; }
        public string Address1 { get; set; }
        public string PrimaryContact { get; set; }
        public string SecondaryContact { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
    }
}