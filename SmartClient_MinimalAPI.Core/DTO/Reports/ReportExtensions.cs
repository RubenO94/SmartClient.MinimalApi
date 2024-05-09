using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ClientName = registerViewModel?.Client.Name,
                PartnerClientName = string.IsNullOrEmpty(registerViewModel?.PartnerClient.Name) ? null : registerViewModel?.PartnerClient.Name,
                Subject = registerViewModel?.Subject,
                TicketID = registerViewModel?.TicketID ?? 0L,
                CreatedAt = registerViewModel?.Creation ?? null,
                UserImageBase64 = string.IsNullOrEmpty(registerViewModel?.UserImage) ? null : registerViewModel?.UserImage
            };
        }
    }
}
