using System;
using System.ComponentModel.DataAnnotations;

namespace TCRC.Models.Member
{
    public class BusinessDbaName
    {
        public int BusinessDbaNameId { get; set; }

        public int TcrcId { get; set; }

        [Display(Name = "DBA Name")]
        public string DbaName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}