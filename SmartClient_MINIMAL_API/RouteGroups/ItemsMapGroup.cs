using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClientMinimalApi.Core.ServicesContracts;

namespace SmartClient.MinimalApi.RouteGroups
{
    public static class ItemsMapGroup
    {
        public static RouteGroupBuilder ItemsAPI(this RouteGroupBuilder group)
        {

            group.MapGet("/", (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService) =>
            {
                try
                {
                    // TODO

                    throw new NotImplementedException("Este metodo ainda não foi implementado nesta versão");
                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ItemsMapGroup)}.{ItemsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            return group;
        }
    }
}
