using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Attachments
{
    public class AttachmentResponseDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int AttachementID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Extension { get; set; }
        public long ? Size { get; set; }
    }
}
