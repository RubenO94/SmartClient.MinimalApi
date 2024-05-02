namespace SmartClientMinimalApi.RouteGroups
{
    public static class TicketsMapGroup
    {
        public static RouteGroupBuilder TicketsAPI(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (HttpContext context) =>
            {
                await context.Response.WriteAsync("GET - Hello World");
            });

            group.MapPost("/", async (HttpContext context, IFormFile formFile) =>
            {
                await context.Response.WriteAsync("POST - Hello World");
            });


            return group;
        }
    }
}
