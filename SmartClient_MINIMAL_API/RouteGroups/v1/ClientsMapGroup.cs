using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalApi.EndpointFilters;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;

namespace SmartClient.MinimalApi.RouteGroups.v1
{
    public static class ClientsMapGroup
    {
        public static RouteGroupBuilder ClientsV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .AddEndpointFilter<UserValidationFilter>()
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
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{ClientsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            });


            return group;
        }
    }
}
