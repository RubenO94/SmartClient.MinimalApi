using SmartClientMinimalApi.Core.Domain.ApplicationContracts;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;

namespace SmartClientMinimalApi.Core.Services
{
    /// <summary>
    /// Implementação do contrato de serviço para interagir com o SmartClientWS.
    /// </summary>
    public class SmartClientWebService : ISmartClientWebService
    {
        private readonly IAppSettings _appSettings;
        private readonly SMServiceClient _serviceClient;

        /// <summary>
        /// Inicializa uma nova instância de SmartClientWebService.
        /// </summary>
        /// <param name="appSettings">As configurações da aplicação.</param>
        public SmartClientWebService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _serviceClient = new SMServiceClient(SMServiceClient.EndpointConfiguration.BasicHttpBinding_ISMService, _appSettings.SmartClientWSBaseURL);
        }

        /// <summary>
        /// Obtém o cliente do serviço SmartClientWS.
        /// </summary>
        /// <returns>O cliente do serviço SmartClientWS.</returns>
        public SMServiceClient GetService()
        {
            return _serviceClient;
        }
    }
}
