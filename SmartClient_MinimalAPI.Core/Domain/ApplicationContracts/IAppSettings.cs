namespace SmartClientMinimalApi.Core.Domain.ApplicationContracts
{
    /// <summary>
    /// Define um contrato para acesso às configurações da aplicação.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Obtém a URL base para o SmartClientWS.
        /// </summary>
        string SmartClientWSBaseURL { get; }
        
        /// <summary>
        /// 
        /// </summary>
        int JwtExpirationMinutes { get; }

        /// <summary>
        /// 
        /// </summary>
        string JwtIssuer { get; }


        /// <summary>
        /// 
        /// </summary>
        string JwtAudience { get; }

        /// <summary>
        /// 
        /// </summary>
        string JwtKey { get; }
    }
}
