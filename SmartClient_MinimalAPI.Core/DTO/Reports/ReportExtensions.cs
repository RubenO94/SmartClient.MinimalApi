using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.AspNetCore.StaticFiles;
using SmartClient.MinimalAPI.Core.DTO.Attachments;
using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients;
using SmartClient.MinimalAPI.Core.DTO.Contracts;
using SmartClient.MinimalAPI.Core.DTO.Items.Products;
using SmartClient.MinimalAPI.Core.DTO.Reports.ReportDescriptions;
using SmartClient.MinimalAPI.Core.DTO.Reports.ReportImages;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Attachment = SmartClientWS.Attachment;

namespace SmartClient.MinimalAPI.Core.DTO.Reports
{
    public static class ReportExtensions
    {
        public static ReportResponseDTO ToResponseDTO(this RegisterViewModel registerViewModel)
        {
            return new ReportResponseDTO
            {
                ReportID = registerViewModel.FormID,
                ID = registerViewModel?.ID ?? Guid.Empty,
                ClientName = registerViewModel?.Client?.Name,
                PartnerClientName = string.IsNullOrEmpty(registerViewModel?.PartnerClient?.Name) ? null : registerViewModel?.PartnerClient?.Name,
                Subject = registerViewModel?.Subject,
                TicketID = registerViewModel?.TicketID ?? 0L,
                CreatedAt = registerViewModel?.Creation ?? null,
                UserImageBase64 = string.IsNullOrEmpty(registerViewModel?.UserImage) ? null : registerViewModel?.UserImage
            };
        }

        public static ReportDetailResponseDTO ToDetailResponseDTO(this RegisterViewModel registerViewModel)
        {
            return new ReportDetailResponseDTO
            {
                ReportID = registerViewModel.FormID,
                ID = registerViewModel?.ID ?? Guid.Empty,
                ClientName = registerViewModel?.Client?.Name,
                PartnerClientName = string.IsNullOrEmpty(registerViewModel?.PartnerClient?.Name) ? null : registerViewModel?.PartnerClient?.Name,
                Subject = registerViewModel?.Subject,
                TicketID = registerViewModel?.TicketID ?? 0L,
                CreatedAt = registerViewModel?.Creation ?? null,
                UserImageBase64 = string.IsNullOrEmpty(registerViewModel?.UserImage) ? null : registerViewModel?.UserImage,
                Address = registerViewModel?.Address,
                CanEdit = registerViewModel?.CanEdit ?? false,
                Attachments = registerViewModel?.Attachments?.Select(attch => attch.ToResponseDTO()).ToList(),
                Client = registerViewModel?.Client?.ToResponseDTO(),
                Contract = registerViewModel?.Contract?.ToResponseDTO(),
                MaintenanceContract = registerViewModel?.MaintenanceContract ?? false,
                ClientEmail = registerViewModel?.ClientEmail,
                ClientType = registerViewModel?.TipoCliente,
                Contact = registerViewModel?.Contact,
                ContractNumber = registerViewModel?.ContractNumber ?? default,
                Date = registerViewModel?.Date,
                Descriptions = registerViewModel?.Descriptions?.Select(description => description.ToResponseDTO()).ToList(),
                Email = registerViewModel?.Email,
                FinalClient = registerViewModel?.FinalClient,
                Finished = registerViewModel?.Finished ?? false,
                ReportsMerged = registerViewModel?.Merged?.Select(report => report.ToResponseDTO()).ToList(),
                IVA = registerViewModel?.IVA ?? default,
                PartnerClient = registerViewModel?.PartnerClient?.ToResponseDTO(),
                Kms = registerViewModel?.Kms ?? default,
                NewSignatureRequired = registerViewModel?.NewSignatureRequired ?? false,
                NewTicketStatus = registerViewModel?.NewTicketStatus ?? default,
                Person = registerViewModel?.Person,
                State = registerViewModel?.State ?? default,
                Present = registerViewModel?.Present ?? default,
                Products = registerViewModel?.Products?.Select(product => product.ToResponseDTO()).ToList() ?? default,
                Total = registerViewModel?.Total ?? default,
                Signature = registerViewModel?.Signature ?? default,
                Signed = registerViewModel?.Signed ?? default,
                ToBill = registerViewModel?.Faturar ?? default,
                TotalTime = registerViewModel?.TotalTime ?? default,
                Vehicle = registerViewModel?.Vehicle ?? default,
                ReportImages = new List<ReportImageResponseDTO>()
            };
        }

        public static ReportDescriptionResponseDTO ToResponseDTO(this ServiceDescription description)
        {
            return new ReportDescriptionResponseDTO
            {
                Bill = description.Bill,
                Description = description.Description,
                End = description.End,
                RowID = description.RowID,
                Start = description.Start,
                Total = description.Total,
                UserID = description.UserID

            };
        }

        public static List<ReportImageResponseDTO?>? ToReportImagesResponseDTO(this IEnumerable<Attachment?> attachments, Guid reportID, int userID, ISmartClientWebService clientWebService, FileExtensionContentTypeProvider fileExtensionContentType)
        {
            var result = attachments.Select(async attach =>
            {
                if (attach == null || !fileExtensionContentType.TryGetContentType(attach.Extension, out string contentType) || !contentType.StartsWith("image/"))
                    return null;

                var downloadResult = await clientWebService.GetService().DownloadFormFileAsync(attach.Name + attach.Extension, reportID, 0, null, userID);
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        downloadResult.FileByteStream.CopyTo(memoryStream);

                        // Convert byte[] to Base64 String
                        string base64String = Convert.ToBase64String(memoryStream.ToArray());

                        return new ReportImageResponseDTO()
                        {
                            Id = Guid.NewGuid(),
                            Name = downloadResult.FileName,
                            Base64 = $"data:{contentType};base64,{base64String}"
                        };
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
            });

            var reportImages = result.Select(task => task.Result).ToList();

            return reportImages;
        }
    }
}
