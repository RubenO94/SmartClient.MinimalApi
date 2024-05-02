using Microsoft.AspNetCore.Mvc;
using SmartClientMinimalApi.Core.Domain.Resources;
using SmartClientMinimalApi.Core.DTO;
using SmartClientMinimalApi.Core.ServicesContracts;
using System.Net;

namespace SmartClientMinimalApi.RouteGroups
{
    public static class AuthenticationMapGroup
    {
        public static RouteGroupBuilder AuthenticationAPI(this RouteGroupBuilder group)
        {

            group.MapGet("/", async (HttpContext context, ISmartClientWebService clientWebService) =>
            {
                await context.Response.WriteAsync("GET - Hello World");
            });

            group.MapPost("/", async (HttpContext context, ISmartClientWebService clientWebService, IJwtService jwtService, [FromBody] AuthenticationRequestDTO request) =>
            {
                var result = await clientWebService.GetService().LoginRequestAsync(request.Email, request.Password, null);

                if (result == null)
                {
                    var error = DataStateExtensions.CreateDataFailed(new Error("Ocorreu um erro inesperado"));
                    return Results.BadRequest(error);
                }
                else
                {
                    var response = result.ToAuthenticationResponseDTO(jwtService);

                    return Results.Ok(response.ToDataSuccess(HttpStatusCode.OK));
                }
            });
            return group;
        }
    }
}
