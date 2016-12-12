using System;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models.FileClaim
{
    public class ClaimPassenger
    {
        public int ClaimPassengerId { get; set; }

        public int ClaimId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}