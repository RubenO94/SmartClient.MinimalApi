using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.StockMovements;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.Domain.Resources;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientMinimalApi.RouteGroups;
using SmartClientWS;
using System.Security.Claims;

namespace SmartClient.MinimalApi.RouteGroups
{
    public static class StockMovementsMapGroup
    {

        public static RouteGroupBuilder StockMovementsAPI(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {

                try
                {
                    var filters = new SmartClientWS.FilterRequest { Page = Page, PageSize = PageSize, Take = PageSize, Skip = PageSize * Page };

                    var response = await clientWebService.GetService().GetStockMovementsAsync(filters, true, false);


                    if (response == null || !response.success)
                    {
                        throw new ArgumentException(response?.message ?? "Não foi possivel comunicar com o Web Service");
                    }

                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }



            });

            group.MapGet("/{id:int}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int id) =>
            {

                try
                {
                    if (id <= 0)
                    {
                        return ResultExtensions.ResultFailed($"Movimento com o ID {id} é inválido");
                    }
                    
                    var response = await clientWebService.GetService().GetStockMovementAsync(id);


                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    var dto = response.ToResponseDTO();

                    return dto.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }



            });

            group.MapGet("/temp", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {

                try
                {
                    var response = await clientWebService.GetService().GetTemporaryStockMovementsAsync();


                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    var stockMovementsDTO = response.Select(sm => sm.ToResponseDTO()).ToList();
                    return stockMovementsDTO.ToResult(Page, PageSize);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            group.MapPost("/temp", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService) =>
            {
                var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                if (!isValid)
                {
                    return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                }

                try
                {
                    var response = await clientWebService.GetService().CreateTemporaryStockMovementAsync(new StockMovement(), userID, true);


                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            return group;
        }
    }
}
