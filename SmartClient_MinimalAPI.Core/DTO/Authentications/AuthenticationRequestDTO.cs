using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Authentications
{
    public class AuthenticationRequestDTO
    {
        [Required(ErrorMessage ="Username não pode ser vazio")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Palavra-passe não pode ser vazia")]
        public string? Password { get; set; }
    }
}
