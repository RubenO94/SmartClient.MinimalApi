using SmartClient.MinimalAPI.Core.DTO.Items.Products;
using SmartClient.MinimalAPI.Core.DTO.Reports.ReportDescriptions;
using System.ComponentModel.DataAnnotations;

namespace SmartClient.MinimalAPI.Core.DTO.Reports
{
    public class ReportUpdateRequestDTO
    {
        [Required(ErrorMessage = $"Parametro {nameof(ID)} não pode ser vazio ou nulo")]
        public Guid ID { get; set; }
        public int IVA { get; set; }
        public DateTime? Date { get; set; }
        public string? Address { get; set; }
        public string? Subject { get; set; }
        public int ClientID { get; set; }
        public bool IsToInvoice { get; set; }
        public decimal Kms { get; set; }
        public bool Present { get; set; }
        public string? State { get; set; }
        public string? Vehicle { get; set; }
        public bool SendEmail { get; set; }
        public string? Person { get; set; }
        public string? Contact { get; set; }
        public bool Finished { get; set; }
        public string? Email { get; set; }
        public string? FinalClient { get; set; }
        public long ContractID { get; set; }
        public DateTime? ContractExpiration { get; set; }
        public List<int>? MergedForms { get; set; }
        public int PartnerClientID { get; set; }
        public bool FastServiceReportFlag { get; set; }
        public bool AddReportToTicketAttachments { get; set; }
        public string? Observations { get; set; }
        public bool MaintenanceContract { get; set; }
        public List<ProductUpdateRequestDTO>? Products { get; set; }
        public List<ReportDescriptionUpdateRequestDTO>? Descriptions { get; set; }
    }
}
