using Microsoft.IdentityModel.Tokens;
using SmartClientMinimalApi.Core.Domain.ApplicationContracts;
using SmartClientMinimalApi.Core.DTO;
using SmartClientMinimalApi.Core.ServicesContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartClientMinimalApi.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IAppSettings _appSettings;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="JwtService"/>.
        /// </summary>
        /// <param name="appSettings">O objeto de configuração para aceder às definições da aplicação.</param>
        public JwtService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public AuthenticationResponseDTO CreateJwtToken(SmartUserDTO user)
        {
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_appSettings.JwtExpirationMinutes);

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email?.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName?.ToString() ?? string.Empty),
            };

            // Configurar a chave de segurança e as credenciais de assinatura
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Gera o token JWT
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
              _appSettings.JwtIssuer,
              _appSettings.JwtAudience,
                claims,
                expires: expirationTime,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Escreve o token como uma string
            string token = tokenHandler.WriteToken(tokenGenerator);

            // Retorna o AuthenticationResponse com o token e as informações do utilizador
            return new AuthenticationResponseDTO(user, token, expirationTime, null);
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _appSettings.JwtAudience,
                ValidateIssuer = true,
                ValidIssuer = _appSettings.JwtIssuer,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtKey)),

                ValidateLifetime = false //should be false
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }

}
