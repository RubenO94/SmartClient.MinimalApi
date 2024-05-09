using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.Attachments;
using SmartClient.MinimalAPI.Core.DTO.StockMovements;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.Domain.Resources;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientMinimalApi.RouteGroups;
using SmartClientWS;
using System.Security.Claims;

namespace SmartClient.MinimalApi.RouteGroups.v1
{
    public static class StockMovementsMapGroup
    {

        public static RouteGroupBuilder StockMovementsV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .MapToApiVersion(1)
                .WithOpenApi(options =>
            {
                options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "StockMovements" } };

                return options;
            });

            // Endpoints

            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {

                try
                {
                    var filters = new FilterRequest { Page = Page, PageSize = PageSize, Take = PageSize, Skip = PageSize * Page };

                    var response = await clientWebService.GetService().GetStockMovementsAsync(filters, true, false);


                    if (response == null || !response.success)
                    {
                        throw new ArgumentException(response?.message ?? "Não foi possivel comunicar com o Web Service");
                    }

                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
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
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }



            });


            group.MapPost("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromBody] StockMovementAddRequestDTO request, [FromQuery] bool finish) =>
            {

                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    if (request == null)
                    {
                        return ResultExtensions.ResultFailed($"Corpo da requisição em falta");
                    }

                    var stockMovement = request.ToStockMovement();

                    if (finish)
                    {
                        var createResponse = await clientWebService.GetService().CreateStockMovementAsync(stockMovement, userID);

                        return createResponse.ToResult();
                    }

                    var updateTempResponse = await clientWebService.GetService().CreateTemporaryStockMovementAsync(stockMovement, userID, true);


                    if (updateTempResponse == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    return updateTempResponse.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
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
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
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
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            group.MapDelete("/temp/{id:int}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int id) =>
            {
                var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                if (!isValid)
                {
                    return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                }

                if (id <= 0)
                {
                    return ResultExtensions.ResultFailed($"Movimento temporário com o ID {id} é inválido");
                }

                try
                {
                    var response = await clientWebService.GetService().DeleteTemporaryStockMovementAsync(id);


                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    return response.ToResult(StatusCodes.Status204NoContent);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            group.MapGet("temp/{id:int}/files", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int id, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {
                var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                if (!isValid)
                {
                    return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                }

                if (id <= 0)
                {
                    return ResultExtensions.ResultFailed($"O parâmetro '{nameof(id)}' não é válido");
                }

                try
                {

                    var response = await clientWebService.GetService().GetStockMovementAttachmentsAsync(id, true, false);


                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    var attachementsDto = response.Select(attach => attach.ToResponseDTO()).ToList();
                    return attachementsDto.ToResult(Page, PageSize);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            group.MapPost("temp/{id:int}/files", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int id, IFormFile file) =>
            {
                var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                if (!isValid)
                {
                    return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                }
                
                if(file == null || file.Length <= 0)
                {
                    return ResultExtensions.ResultFailed($"O parâmetro '{nameof(file)}' é obrigatório");
                }

                try
                {

                    using var fileStream = file.OpenReadStream();

                    var remoteFileInfo = new RemoteFileInfo()
                    {
                        ExternalLink = false,
                        FileByteStream = fileStream,
                        FileID = id,
                        FileName = file.FileName,
                        Length = file.Length,
                        FormID = Guid.Empty,
                        ResponseType = 0,
                        TicketID = 0,
                        TicketSession = null,
                        UserID = userID
                    };

                    var response = await clientWebService.GetService().UploadStockMovementFileAsync(remoteFileInfo);


                    if (response == null)
                    {
                        throw new ArgumentException("Não foi possivel comunicar com o Web Service");
                    }

                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });


            group.MapDelete("temp/{id:int}/files/{fileId:int}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int id, int fileId) =>
            {
                var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                if (!isValid)
                {
                    return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                }

                if (id <= 0)
                {
                    return ResultExtensions.ResultFailed($"O parâmetro '{nameof(id)}' com valor '{id}' não é válido");
                }

                if (fileId <= 0)
                {
                    return ResultExtensions.ResultFailed($"O parâmetro '{nameof(fileId)}' com valor '{fileId}' não é válido");
                }

                try
                {

                    var response = await clientWebService.GetService().RemoveStockMovementFileByIDAsync(fileId);

                    if (!response)
                    {
                        ResultExtensions.ResultFailed($"Não foi possivél eliminar o ficheiro com o ID '{fileId}' do movimento de stock '{id}'");
                    }

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(StockMovementsMapGroup)}.{StockMovementsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            });

            return group;
        }
    }
}
