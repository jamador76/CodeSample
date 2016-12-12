using System;

namespace TCRC.Domain.DTOs
{
    public sealed class SOTReportDTO
    {
        public int? TcrcID { get; set; }
        public int? SotID { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}