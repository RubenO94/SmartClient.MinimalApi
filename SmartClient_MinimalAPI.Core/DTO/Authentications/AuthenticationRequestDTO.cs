using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Authentications
{
    public class AuthenticationRequestDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
