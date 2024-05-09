using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients;
using SmartClient.MinimalAPI.Core.DTO.Contracts.ContractInvoices;
using SmartClient.MinimalAPI.Core.DTO.Contracts.ContractLimits;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.Contracts
{
    public class ContractResponseDTO
    {
        public long ContractID { get; set; }
        
        public DateTime? Start {  get; set; }
        public DateTime? End { get; set; }
        public bool Active { get; set; }
        public bool HasOverduePayments { get; set; }
        public bool IsInvoiced { get; set; }
        public bool Requisition {  get; set; }
        public decimal Total {  get; set; }
        public ClientResponseDTO? Client { get; set; }
        public PartnerClientResponseDTO? Partner { get; set; }
        public ContractType? Type { get; set; }
        public List<ContractLimitResponseDTO>? Limits { get; set; }
        public ContractRenewal? Renewal { get; set; }
        public SmartUserResponseDTO? SmartUser { get; set; }
        public List<ContractInvoiceResponseDTO>? Invoices { get; set; }
        public string? Notes { get; set; }
        public List<string>? NotifyEmails { get; set; }
        public int DaysBeforeExpirationAlert { get; set; }
    }
}
