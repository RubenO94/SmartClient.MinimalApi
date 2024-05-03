using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartClientMinimalApi.Core.Domain.Resources;
using SmartClientMinimalApi.Core.DTO;
using SmartClientMinimalApi.Core.ServicesContracts;
using System.Net;
using System.Security.Claims;

namespace SmartClientMinimalApi.RouteGroups
{
    public static class AuthenticationMapGroup
    {
        public static RouteGroupBuilder AuthenticationAPI(this RouteGroupBuilder group)
        {
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

                    return Results.Ok(response.ToDataSuccess());
                }
            })
                .AllowAnonymous()
                 .WithName("Login Request")
               .WithOpenApi(x => new OpenApiOperation(x)
               {
                   Summary = "Iniciar sessão",
                   Description = "Endpoint para iniciar sessão",
                   Tags = new List<OpenApiTag> { new() { Name = "Authentication" } },
                   RequestBody = new OpenApiRequestBody()
                   {
                       Content = new Dictionary<string, OpenApiMediaType>
                       {
                           ["application/json"] = new OpenApiMediaType
                           {
                               Schema = new OpenApiSchema
                               {
                                   Type = "object",
                                   Required = new HashSet<string>() { "email", "password" },
                                   Properties = new Dictionary<string, OpenApiSchema>
                                   {
                                       ["email"] = new OpenApiSchema { Type = "string", Format = "email" },
                                       ["password"] = new OpenApiSchema { Type = "string", Format = "password" }
                                   }
                               }
                           }
                       }
                   }
               });
            return group;
        }
    }
}
