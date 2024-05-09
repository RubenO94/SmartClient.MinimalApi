using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalApi.EndpointFilters;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.Suppliers;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;

namespace SmartClient.MinimalApi.RouteGroups.v1
{
    public static class SuppliersMapGroup
    {
        public static RouteGroupBuilder SuppliersV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .AddEndpointFilter<UserValidationFilter>()
                .MapToApiVersion(1)
                .WithOpenApi(options =>
                {
                    options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "Suppliers" } };

                    return options;
                });

            // Endpoints:

            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {

                try
                {
                    var response = await clientWebService.GetService().GetSuppliersAsync();

                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    var suppliersDto = response.Select(supplier => supplier.ToResponseDTO()).ToList();
                    return suppliersDto.ToResult(Page, PageSize);


                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{SuppliersV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            });


            return group;
        }
    }
}
