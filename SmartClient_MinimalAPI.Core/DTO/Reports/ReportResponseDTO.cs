using SmartClient.MinimalAPI.Core.DTO.Clients;
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

    public class ReportDetailResponseDTO : ReportResponseDTO
    {
        public bool CanEdit { get; set; }
        public ClientResponseDTO? Client { get; set; }
        public string? Person { get; set; }
        public string? Contact { get; set; }
        public string? ClientType { get; set; }
        public string? Address { get; set; }
        public DateTime? Date { get; set; }
        public bool Present { get; set; }
        public string? Vehicle { get; set; }
        public decimal Kms { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
    }
}
