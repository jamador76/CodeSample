using System;

namespace TCRC.Domain.DTOs
{
    public sealed class ClaimStatusTypeDTO
    {
        public int ClaimStatusTypeId { get; set; }
        public string ClaimStatusTypeName { get; set; }
        public string EmailTemplate { get; set; }
    }
}