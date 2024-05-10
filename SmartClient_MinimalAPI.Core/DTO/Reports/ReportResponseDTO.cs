using SmartClient.MinimalAPI.Core.DTO.Attachments;
using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients;
using SmartClient.MinimalAPI.Core.DTO.Contracts;
using SmartClient.MinimalAPI.Core.DTO.Items.Products;
using SmartClient.MinimalAPI.Core.DTO.Reports.ReportDescriptions;
using SmartClient.MinimalAPI.Core.DTO.Reports.ReportImages;
using SmartClientWS;
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
        public PartnerClientResponseDTO? PartnerClient { get; set; }
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
        public string? ToBill { get; set; }
        public string? Signature { get; set; }
        public bool NewSignatureRequired { get; set; }
        public bool Signed { get; set; }
        public string? State { get; set; }
        public bool MaintenanceContract { get; set; }
        public long ContractNumber { get; set; }
        public ContractResponseDTO? Contract {  get; set; }
        public string? Email { get; set; }
        public string? ClientEmail { get; set; }
        public string? FinalClient { get; set; }
        public bool Finished { get; set; }
        public int NewTicketStatus { get; set; }
        public double TotalTime { get; set; }
        public List<ReportResponseDTO>? ReportsMerged {  get; set; }     

        public List<ReportDescriptionResponseDTO>? Descriptions { get; set; }
        public List<ProductResponseDTO>? Products { get; set; }
        public List<AttachmentResponseDTO?>? Attachments { get; set; }
        public List<ReportImageResponseDTO>? ReportImages { get; set; }
    }
}
