using System;

namespace TCRC.Domain.DTOs
{
    public sealed class BusinessDbaNameDTO
    {
        public int BusinessDbaNameId { get; set; }
        public int TcrcId { get; set; }
        public string DbaName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}