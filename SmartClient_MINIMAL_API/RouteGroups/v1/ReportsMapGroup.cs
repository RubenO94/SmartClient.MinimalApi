using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.OpenApi.Models;
using SmartClient.MinimalApi.EndpointFilters;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.Reports;
using SmartClient.MinimalAPI.Core.DTO.Reports.ReportImages;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClient.MinimalAPI.Core.Utils.Extensions;
using SmartClientMinimalApi.Core.ServicesContracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartClient.MinimalApi.RouteGroups.v1
{
    public static class ReportsMapGroup
    {
        public static RouteGroupBuilder ReportsV1(this RouteGroupBuilder group)
        {
            // Route Configurations
            group
                .RequireAuthorization()
                .MapToApiVersion(1)
                .WithOpenApi(options =>
                {
                    options.Tags = new List<OpenApiTag> { new OpenApiTag() { Name = "Reports" } };

                    return options;
                });

            // Endpoints

            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20, [FromQuery] bool Finished = false) =>
            {

                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    var filters = new SmartClientWS.FilterRequest { Page = Page, PageSize = PageSize, Take = PageSize, Skip = PageSize * Page };

                    var response = await clientWebService.GetService().GetServicesAsync(string.Empty, userID, true, Finished ? 1 : 0, filters);


                    if (response == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }


                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ReportsMapGroup)}.{ReportsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            });

            group.MapGet("{id:int}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromServices] FileExtensionContentTypeProvider fileExtensionContentTypeProvider, int id) =>
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
                        throw new ArgumentException($"Report com ID {id} não é válido");
                    }

                    var response = await clientWebService.GetService().GetFormByIDAsync(id, userID);


                    if (response == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }

                    response.Observations = response.Observations.TextFromHtml();

                    var attachments = await clientWebService.GetService().GetAttachmentsAsync(response.ID, false);

                    ReportDetailResponseDTO reportDetailDto = response.ToDetailResponseDTO();

                    foreach (var attach in attachments)
                    {
                        if (!fileExtensionContentTypeProvider.TryGetContentType(attach.Extension, out string contentType) || !contentType.StartsWith("image/"))
                            continue;

                        var downloadResult = await clientWebService.GetService().DownloadFormFileAsync(attach.Name + attach.Extension, response.ID, 0, null, userID);
                        try
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                downloadResult.FileByteStream.CopyTo(memoryStream);

                                // Convert byte[] to Base64 String
                                string base64String = Convert.ToBase64String(memoryStream.ToArray());

                                reportDetailDto.ReportImages?.Add(new ReportImageResponseDTO()
                                {
                                    Id = Guid.NewGuid(),
                                    Name = downloadResult.FileName,
                                    Base64 = $"data:{contentType};base64,{base64String}"
                                });
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            downloadResult.FileByteStream.Dispose();
                        }
                    }

                    return reportDetailDto.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ReportsMapGroup)}.{ReportsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            });

            group.MapGet("{id:int}/checkIn", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int id, [FromQuery] DateTime? date) =>
            {

                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    if (date == null || !date.HasValue) return ResultExtensions.ResultFailed($"QueryString '{nameof(date)}' é obrigatório");


                    var reportResult = await clientWebService.GetService().GetFormByIDAsync(id, userID);

                    if (reportResult == null) return ResultExtensions.ResultFailed($"Relatório com ID {id} não é válido");


                    var response = await clientWebService.GetService().GetDateCheckInAsync(userID, reportResult.ID, date.Value);


                    if (response == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }


                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ReportsMapGroup)}.{ReportsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            });

            group.MapPut("{id:int}", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, int id, [FromQuery] bool finish, [FromBody] ReportUpdateRequestDTO reportUpdate) =>
            {

                try
                {
                    var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.User);

                    if (!isValid)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    if(reportUpdate == null) return ResultExtensions.ResultFailed($"Corpo do relatório atulizado é obrigatório");



                    if (finish)
                    {
                        // TODO
                    }

                    var reportResult = await clientWebService.GetService().GetFormByIDAsync(id, userID);

                    if (reportResult == null) return ResultExtensions.ResultFailed($"Relatório com ID {id} não é válido");


                    var response = await clientWebService.GetService().GetDateCheckInAsync(userID, reportResult.ID, date.Value);


                    if (response == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }


                    return response.ToResult();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ReportsMapGroup)}.{ReportsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            });

            group.MapGet("deleteReasons", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] int Page = 0, [FromQuery] int PageSize = 20) =>
            {

                try
                {


                    var response = await clientWebService.GetService().GetReportDeleteReasonsAsync();


                    if (response == null)
                    {
                        throw new ArgumentNullException("Não foi possivel comunicar com o Web Service");
                    }


                    return response.ToResult(Page, PageSize);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(ReportsMapGroup)}.{ReportsV1}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }


            }).AddEndpointFilter<UserValidationFilter>();


            return group;
        }
    }
}
