using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalApi.EndpointFilters;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClientMinimalApi.Core.ServicesContracts;

namespace SmartClient.MinimalApi.RouteGroups
{
    public static class ItemsMapGroup
    {
        public static RouteGroupBuilder ItemsV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .AddEndpointFilter<UserValidationFilter>()
                .MapToApiVersion(1)
                .WithOpenApi(options =>
            {
                options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "Items" } };

                return options;
            });

            // Endpoints

            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] string? filter, [FromQuery]int Page = 0, [FromQuery] int PageSize = 20) =>
            {
                try
                {
                    if (filter == null) filter = string.Empty;

                    var offSetNum = Page * PageSize;

                    var response = await clientWebService.GetService().GetAllItemsAsync(0, filter, offSetNum, PageSize, true, false, -1, false);


                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    return response.ToResult();


                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ItemsMapGroup)}.{ItemsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            group.MapGet("/serialNumbers/{id:serialNumber}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, string id) =>
            {
                try
                {

                   if(string.IsNullOrEmpty(id))
                    {
                        return ResultExtensions.ResultFailed($"Numero de Série com ID {id} não é válido");
                    }

                    var response = await clientWebService.GetService().GetSerialNumberInfoAsync(id);


                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    return response.ToResult();


                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ItemsMapGroup)}.{ItemsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            return group;
        }
    }
}
