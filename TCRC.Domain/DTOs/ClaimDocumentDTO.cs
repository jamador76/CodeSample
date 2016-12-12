using System;

namespace TCRC.Domain.DTOs
{
    public sealed class ClaimDocumentDTO
    {
        public int ClaimDocumentId { get; set; }
        public int ClaimId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}