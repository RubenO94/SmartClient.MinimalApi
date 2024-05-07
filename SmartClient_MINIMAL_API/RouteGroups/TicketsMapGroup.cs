using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO;
using SmartClient.MinimalAPI.Core.DTO.Tickets;
using SmartClient.MinimalAPI.Core.Utils;
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

            group.NewApiVersionSet()
                .HasApiVersion(new Asp.Versioning.ApiVersion(1, 0))
                .ReportApiVersions()
                .Build();

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
                    filters.Filter = new Filter();
                    filters.Filter.filters = new List<Filter> { new Filter { field = "TicketType.CreateServiceReport", value = true, @operator = "eq", logic = "AND" } };

                    var result = await clientWebService.GetService().GetTicketsAsync("Pendente", userID, true, false, 0, null, null, null, null, filters);


                    if (result == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }

                    var tickets = result.Tickets.Select(tkt => tkt.ToResponseDTO()).ToList();
                    return result.ToResult(tickets);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(TicketsMapGroup)}.{TicketsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            });

            group.MapGet("/repairTickets/{ClientID:int}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int ClientID, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {
                try
                {
                    var userID = Convert.ToInt32(context.User.FindFirstValue(ClaimTypes.NameIdentifier));

                    if (userID <= 0)
                    {
                        throw new ArgumentException($"Utilizador com ID {userID} não é válido");
                    }

                    if (ClientID <= 0)
                    {
                        throw new ArgumentException($"Cliente com ID {userID} não é válido");
                    }

                    var response = await clientWebService.GetService().GetBasicRepairTicketsAsync(ClientID);


                    if (response == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }

                    var ticketsDTO = response.Select(tkt => tkt.ToResponseDTO()).ToList();
                    return ticketsDTO.ToResult(Page, PageSize);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(TicketsMapGroup)}.{TicketsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });


            return group;
        }
    }
}
