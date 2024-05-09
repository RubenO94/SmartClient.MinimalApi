using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Reports.ReportImages
{
    public class ReportImageResponseDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Base64 { get; set; }
    }
}
