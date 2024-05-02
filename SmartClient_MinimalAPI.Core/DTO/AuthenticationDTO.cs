using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SmartClientMinimalApi.Core.DTO
{
    public abstract class AuthenticationDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SmartUserDTO? User { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? ExpirationTime { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Password { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Roles { get; set; }
        public bool isAdmin { get; set; } = false;


        protected AuthenticationDTO(string? email = null, string? password = null, SmartUserDTO? smartUser = null, string? token = null, DateTime? expirationTime = null, List<string>? roles = null)
        {
            Email = email;
            Password = password;
            Token = token;
            ExpirationTime = expirationTime;
            User = smartUser;
            Roles = roles;
            isAdmin = Roles?.Contains("Admin") ?? false;
        }
    }


    public class AuthenticationResponseDTO : AuthenticationDTO
    {
        public AuthenticationResponseDTO(SmartUserDTO smartUser, string token, DateTime expirationTime, List<string> roles) : base(null, null, smartUser, token, expirationTime, roles)
        {
        }
    }

    public class AuthenticationRequestDTO : AuthenticationDTO
    {

        public AuthenticationRequestDTO(string email, string password) : base(email, password, null, null, null)
        {
        }
    }


    public static class AuthenticationExtensions
    {
        public static AuthenticationResponseDTO ToAuthenticationResponseDTO(this LogInResult result, IJwtService jwtService)
        {
            return jwtService.CreateJwtToken(result.SmartUser.ToDTO());
        }
    }
}
