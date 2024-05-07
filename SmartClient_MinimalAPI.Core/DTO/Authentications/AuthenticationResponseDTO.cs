using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Authentications
{
    public class AuthenticationResponseDTO : ResponseDTO
    {
        public SmartUserResponseDTO? User { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpirationTime { get; set; }
        [JsonIgnore]
        public List<string>? Roles { get; set; }
        public bool isAdmin { get; set; } = false;

        public AuthenticationResponseDTO(SmartUserResponseDTO smartUser, string token, DateTime expirationTime, List<string> roles) 
        { 
           User = smartUser;
            Token = token;
            ExpirationTime = expirationTime;
            Roles = roles;
            isAdmin = roles.Contains("Admin");
        }

    }
}
