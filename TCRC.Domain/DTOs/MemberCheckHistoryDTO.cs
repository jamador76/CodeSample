using System;

namespace TCRC.Domain.DTOs
{
    public sealed class MemberCheckHistoryDTO
    {
        public DateTime CreateDate { get; set; }
        public long? CheckNumber { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}