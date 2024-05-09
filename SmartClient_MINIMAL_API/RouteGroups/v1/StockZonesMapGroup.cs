using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalApi.EndpointFilters;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.ServicesContracts;

namespace SmartClient.MinimalApi.RouteGroups
{
    public static class StockZonesMapGroup
    {
        public static RouteGroupBuilder StockZonesV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .AddEndpointFilter<UserValidationFilter>()
                .MapToApiVersion(1)
                .WithOpenApi(options =>
                {
                    options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "StockZones" } };

                    return options;
                });

            // Endpoints:

            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {

                try
                {
                    var response = await clientWebService.GetService().GetStockZonesAsync();

                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    var stockZonesDto = response.Select(zone => zone.ToResponseDTO()).ToList(); 
                    return stockZonesDto.ToResult(Page, PageSize);


                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockZonesMapGroup)}.{StockZonesV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            });


            return group;
        }
    }
}
