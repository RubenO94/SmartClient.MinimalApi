using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.SmartUsers
{
    public static class SmartUserExtensions
    {
        public static SmartUserResponseDTO ToResponseDTO(this SmartUser smartUser)
        {
            return new SmartUserResponseDTO()
            {
                UserID = smartUser.UserID,
                UserName = smartUser.UserName,
                Email = smartUser.Email,
                FullName = smartUser.Name,
            };
        }
    }
}
