using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Reports
{
    public class ReportResponseDTO
    {
        public long ReportID { get; set; }
        public Guid? ID { get; set; }
        public string? ClientName { get; set; }
        public string? PartnerClientName { get; set; }
        public long TicketID { get; set; }
        public string? Subject { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserImageBase64 { get; set; }
    }
}
