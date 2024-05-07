using System;
using System.Linq;
using System.Security.Claims;

namespace SmartClient.MinimalAPI.Core.Utils
{
    /// <summary>
    /// Classe utilitária para verificar a validade de um utilizador autenticado com base nas reivindicações fornecidas.
    /// </summary>
    public static class AuthenticationUtils
    {

        /// /// <summary>
        /// Verifica se o utilizador autenticado é válido com base nas reivindicações fornecidas.
        /// </summary>
        /// <param name="user">O objeto ClaimsPrincipal que representa o utilizador autenticado.</param>
        /// <returns>True se o utilizador autenticado for válido; caso contrário, False.</returns>
        public static (bool IsValid, int UserID) CheckAuthenticatedUser(ClaimsPrincipal user)
        {
            try
            {
                if (user == null || !user.Identity!.IsAuthenticated)
                    return (false, 0);

                if (!user.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                    return (false, 0);

                if (!int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out int userID) || userID <= 0)
                    return (false, 0);

                return (true, userID);
            }
            catch
            {
                return (false, 0);
            }
        }
    }
}
