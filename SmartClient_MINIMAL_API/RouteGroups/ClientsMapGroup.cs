using SmartClientMinimalApi.Core.ServicesContracts;

namespace SmartClient.MinimalApi.RouteGroups
{
    public static class ClientsMapGroup
    {
        public static RouteGroupBuilder ClientsAPI(this RouteGroupBuilder group)
        {

            group.MapGet("/", (HttpContext context, ISmartClientWebService clientWebService) =>
            {

            });


            return group;
        }
    }
}
