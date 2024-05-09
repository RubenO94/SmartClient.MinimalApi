using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.Reports;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.ServicesContracts;

namespace SmartClient.MinimalApi.RouteGroups.v1
{
    public static class ReportsMapGroup
    {
        public static RouteGroupBuilder ReportsV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .MapToApiVersion(1)
                .WithOpenApi(options =>
                {
                    options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "Reports" } };

                    return options;
                });

            // Endpoints

            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {

                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    var filters = new SmartClientWS.FilterRequest { Page = Page, PageSize = PageSize, Take = PageSize, Skip = PageSize * Page };
           
                    var response = await clientWebService.GetService().GetServicesAsync(string.Empty, userID, true, 0, filters);


                    if (response == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }

                    
                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ReportsMapGroup)}.{ReportsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            }).CacheOutput(policy =>
            {
                policy.SetVaryByQuery("Page", "PageSize");
                policy.Expire(TimeSpan.FromSeconds(180));
            });

            
            return group;
        }
    }
}
