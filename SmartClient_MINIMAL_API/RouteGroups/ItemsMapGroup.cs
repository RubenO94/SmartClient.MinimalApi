using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;

namespace SmartClient.MinimalApi.RouteGroups
{
    public static class ItemsMapGroup
    {
        public static RouteGroupBuilder ItemsAPI(this RouteGroupBuilder group)
        {
            // Route OpenApi Configurations
            group
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
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

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

                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ItemsMapGroup)}.{ItemsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            group.MapGet("/serialNumbers/{id:serialNumber}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, string id) =>
            {
                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

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

                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ItemsMapGroup)}.{ItemsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            return group;
        }
    }
}
