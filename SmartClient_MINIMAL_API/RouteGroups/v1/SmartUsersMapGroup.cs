using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalApi.RouteGroups;
using SmartClientMinimalApi.Core.ServicesContracts;
using Microsoft.OpenApi.Models;

namespace SmartClient.MinimalApi.RouteGroups.v1
{
    public static class SmartUsersMapGroup
    {
        public static RouteGroupBuilder SmartUsersV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .MapToApiVersion(1)
                .WithOpenApi(options =>
            {
                options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "SmartUsers" } };

                return options;
            });

            // Endpoints


            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, CancellationToken cancellationToken, ISmartClientWebService clientWebService) =>
            {
                try
                {
                    // TODO
                    await Task.Delay(10_000, cancellationToken);

                    var message = "Finished slow delay of 10 seconds.";

                    return Results.Ok(message);
                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(SmartUsersMapGroup)}.{SmartUsersV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            return group;
        }
    }
}
