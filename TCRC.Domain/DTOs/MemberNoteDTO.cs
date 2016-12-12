using System;

namespace TCRC.Domain.DTOs
{
    public sealed class MemberNoteDTO
    {
        public int MemberNoteId { get; set; }
        public int TcrcId { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
    }
}