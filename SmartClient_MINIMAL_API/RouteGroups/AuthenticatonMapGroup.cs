using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.Authentications;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClientMinimalApi.Core.Domain.Resources;
using SmartClientMinimalApi.Core.ServicesContracts;

namespace SmartClientMinimalApi.RouteGroups
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthenticationMapGroup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static RouteGroupBuilder AuthenticationV1(this RouteGroupBuilder group)
        {

            // Route OpenApi Configurations
            group
                .MapToApiVersion(1)
                .WithOpenApi(options =>
                {
                    options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "Authentication" } };

                    return options;
                });

            // Endpoints

            group.MapPost("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, IJwtService jwtService, [FromBody] AuthenticationRequestDTO request) =>
            {
                try
                {
                    var response = await clientWebService.GetService().LoginRequestAsync(request.Username, request.Password, null);

                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    if (!response.success)
                    {
                        return ResultExtensions.ResultFailed(response.message, false, StatusCodes.Status400BadRequest);
                    }

                    var smartUserDTO = response.SmartUser.ToResponseDTO();
                    var authDTO = jwtService.CreateJwtToken(smartUserDTO, response.role.ToList());

                    return authDTO.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(AuthenticationMapGroup)}.{AuthenticationV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }
            }).AllowAnonymous();

            return group;
        }
    }
}
