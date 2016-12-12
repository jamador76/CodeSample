using System;
using System.Collections.Generic;
using TCRC.Domain.DTOs;

namespace TCRC.BusinessLogic.Services
{
    public interface IAdminService
    {
        void ImportSots(string filePath);
        IList<UserProfileDTO> GetUsers();
        IList<MemberDTO> SearchMembers(MemberSearchDTO model);
    }
}