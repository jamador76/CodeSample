using System;

namespace TCRC.Domain.DTOs
{
    public sealed class BusinessContactDTO
    {
        public int BusinessContactId { get; set; }
        public int TcrcId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string EmailAddress { get; set; }
        public bool PrimaryContactMailRenewal { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string SecondaryFirstName { get; set; }
        public string SecondaryMiddleName { get; set; }
        public string SecondaryLastName { get; set; }
        public string SecondaryTitle { get; set; }
        public string SecondaryEmailAddress { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public string SecondaryFaxNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}