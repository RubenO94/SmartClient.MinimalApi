using SmartClientMinimalApi.Core.ServicesContracts;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace SmartClientMinimalApi.RouteGroups
{
    public static class SmartUsersMapGroup
    {
        public static RouteGroupBuilder SmartUsersAPI(this RouteGroupBuilder group)
        {

            group.MapGet("/", async (HttpContext context, ISmartClientWebService clientWebService) =>
            {
                await context.Response.WriteAsync("GET - Hello World");
            });

            group.MapPost("/", async (HttpContext context, ISmartClientWebService clientWebService) =>
            {
                var result = await clientWebService.GetService().LoginRequestAsync("ruben.oliveira", "!Manuel73981212", null);
                await context.Response.WriteAsync("POST - Hello World");
            });


            return group;
        }
    }
}
