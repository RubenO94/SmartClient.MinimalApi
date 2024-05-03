using SmartClientMinimalApi.Core.DTO;
using System.Security.Claims;

namespace SmartClientMinimalApi.Core.ServicesContracts
{
    /// <summary>
    /// Serviço para criar Tokens de JSON Web (JWT) para autenticação de utilizadores.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Cria um token JWT para o utilizador especificado.
        /// </summary>
        /// <param name="user">O utilizador para o qual o token é gerado.</param>
        /// /// <param name="roles">A lista de roles do utilizador.</param>
        /// <returns>Um objeto AuthResponseDTO contendo o token JWT gerado e as informações do utilizador.</returns>
        AuthenticationResponseDTO CreateJwtToken(SmartUserDTO user, List<string> roles);

        /// <summary>
        /// Obtém o principal (ClaimsPrincipal) a partir de um token JWT.
        /// </summary>
        /// <param name="token">O token JWT a ser analisado.</param>
        /// <returns>Um ClaimsPrincipal representando o utilizador contido no token ou null se o token for inválido.</returns>
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
