using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClientMinimalApi.Core.DTO
{
    public class SmartUserDTO
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set;}

    }


    public static class SmartUserExtensions
    {
        public static SmartUserDTO ToDTO(this SmartUser smartUser)
        {
            return new SmartUserDTO()
            {
                UserID = smartUser.UserID,
                UserName = smartUser.UserName,
                Email = smartUser.Email,
                FullName = smartUser.Name,
            };
        }
    }

}
