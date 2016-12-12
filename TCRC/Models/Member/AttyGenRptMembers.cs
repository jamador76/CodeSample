using System;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models.Member
{
    public class AttyGenReptMembers
    {
        public int TcrcId { get; set; }

        public string AgencyName { get; set; }

        public DateTime ExpireDate { get; set; }
       
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreateDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DateTime ModifyDate
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}