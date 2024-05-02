using SmartClientWS;

namespace SmartClientMinimalApi.Core.ServicesContracts
{
    /// <summary>
    /// Define o contrato para interação com o serviço SmartClientWS.
    /// </summary>
    public interface ISmartClientWebService
    {
        /// <summary>
        /// Inicializa uma ligação com o SmartClientWS através do serviço conectado .svc.
        /// </summary>
        /// <returns>Uma instância de SMServiceClient para interagir com o serviço.</returns>
        SMServiceClient GetService();
    }
}
