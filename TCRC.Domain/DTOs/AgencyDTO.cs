using System;

namespace TCRC.Domain.DTOs
{
    public sealed class AgencyDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PrimaryFirstName { get; set; }
        public string PrimaryMiddleName { get; set; }
        public string PrimaryLastName { get; set; }
        public string SecondaryFirstName { get; set; }
        public string SecondaryMiddleName { get; set; }
        public string SecondaryLastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}