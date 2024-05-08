using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;

namespace SmartClient.MinimalApi.RouteGroups
{
    public static class ClientsMapGroup
    {
        public static RouteGroupBuilder ClientsAPI(this RouteGroupBuilder group)
        {
            // Route OpenApi Configurations
            group
                .MapToApiVersion(1)
                .WithOpenApi(options =>
                {
                    options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "Clients" } };

                    return options;
                });

            // Endpoints:

            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] string? filter, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {

                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    var filters = new FilterRequest
                    {
                        Page = Page,
                        PageSize = PageSize,
                        Take = PageSize,
                        Skip = PageSize * Page,
                        Filter = new Filter()
                    };
                    filters.Filter.filters = new List<Filter> { new Filter { field = "Name", value = filter, @operator = "contains", logic = "AND" } };

                    var response = await clientWebService.GetService().GetClientsAsync(false, false, false, true, null, true, filters);

                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    return response.ToResult();


                }
                catch (Exception ex) 
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{ClientsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                } 
               
                
            });


            return group;
        }
    }
}
