using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.Tickets;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;

namespace SmartClientMinimalApi.RouteGroups
{
    public static class TicketsMapGroup
    {
        public static RouteGroupBuilder TicketsV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .MapToApiVersion(1)
                .WithOpenApi(options =>
            {
                options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "Tickets" } };

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
                    filters.Filter = new Filter();
                    filters.Filter.filters = new List<Filter> { new Filter { field = "TicketType.CreateServiceReport", value = true, @operator = "eq", logic = "AND" } };

                    var result = await clientWebService.GetService().GetTicketsAsync("Pendente", userID, true, false, 0, null, null, null, null, filters);


                    if (result == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }

                    return result.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(TicketsMapGroup)}.{TicketsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            }).CacheOutput(policy =>
            {
                policy.SetVaryByQuery("Page", "PageSize");
                policy.Expire(TimeSpan.FromSeconds(180));
            });

            group.MapGet("/repairTickets/{ClientID:int}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int ClientID, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {
                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    if (ClientID <= 0)
                    {
                        throw new ArgumentException($"Cliente com ID {ClientID} não é válido");
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
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(TicketsMapGroup)}.{TicketsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            group.MapPost("{id:long}/reports", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, long id) =>
            {
                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    if (id <= 0)
                    {
                        throw new ArgumentException($"Ticket com ID {id} não é válido");
                    }

                    var response = await clientWebService.GetService().NewFormAsync(userID, id);


                    if (response == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }

                    
                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(TicketsMapGroup)}.{TicketsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });


            return group;
        }
    }
}
