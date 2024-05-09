using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Attachments
{
    public static class AttachementExtensions
    {
        public static AttachmentResponseDTO ToResponseDTO(this Attachment attachment)
        {
            return new AttachmentResponseDTO()
            {
                AttachementID = attachment.AttachmentID,
                Description = attachment.Description,
                Extension = attachment.Extension,
                Name = attachment.Name,
                Size = attachment.Size,
            };
        }
    }
}
