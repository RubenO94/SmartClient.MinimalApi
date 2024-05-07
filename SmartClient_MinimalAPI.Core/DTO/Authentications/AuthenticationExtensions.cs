using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.Authentications
{
    public static class AuthenticationExtensions
    {
        public static AuthenticationResponseDTO ToResponseDTO(this LogInResult result, IJwtService jwtService)
        {
            return jwtService.CreateJwtToken(result.SmartUser.ToResponseDTO(), result.role.ToList());
        }
    }
}
