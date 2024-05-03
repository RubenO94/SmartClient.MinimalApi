using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartClient.MinimalAPI.Core.DTO;
using SmartClientMinimalApi.Core.Domain.Resources;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;
using System.Security.Claims;

namespace SmartClientMinimalApi.RouteGroups
{
    public static class TicketsMapGroup
    {
        public static RouteGroupBuilder TicketsAPI(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (HttpContext context, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {
                var userID = Convert.ToInt32(context.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (userID <= 0)
                {
                    return Results.BadRequest();
                }

                var filters = new SmartClientWS.FilterRequest { Page = Page, PageSize = PageSize, Take = PageSize, Skip = PageSize * Page };
                filters.Filter = new Filter();
                filters.Filter.filters = new Filter[] { new Filter { field = "TicketType.CreateServiceReport", value = true, @operator = "eq", logic = "AND" } };

                var result = await clientWebService.GetService().GetTicketsAsync("Pendente", userID, true, false, 0, null, null, null, null, filters);


                if (result == null || !result.success)
                {
                    return Results.BadRequest(result.ToDataFailed());
                }

                return Results.Ok(result.ToDataSuccess(result.Tickets.Select(tkt => tkt.ToDTO()).ToList(), System.Net.HttpStatusCode.OK));

            });

            group.MapGet("/repairTickets/{ClientID:int}", async (HttpContext context, ISmartClientWebService clientWebService, int ClientID, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {
                var userID = Convert.ToInt32(context.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (userID <= 0)
                {
                    return Results.BadRequest();
                }

                if(ClientID <= 0)
                {
                    return Results.BadRequest();
                }

                var result = await clientWebService.GetService().GetBasicRepairTicketsAsync(ClientID);


                if (result == null)
                {
                    return Results.BadRequest(result.ToDataFailed());
                }

                return Results.Ok(result.ToDataSuccess());
            });


            return group;
        }
    }
}
