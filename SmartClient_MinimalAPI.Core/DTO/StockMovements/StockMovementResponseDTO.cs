using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClient.MinimalAPI.Core.DTO.StockMovements.StockMovementLines;
using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClient.MinimalAPI.Core.DTO.Suppliers;
using SmartClient.MinimalAPI.Core.DTO.Tickets;
using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.StockMovements
{
    public class StockMovementResponseDTO
    {
        public int StockMovementID { get; set; }
        public int StockMovementTypeID { get; set; }
        public string? StockMovementTypeName { get; set; }
        public ClientResponseDTO? Client { get; set; }
        public SupplierResponseDTO? Supplier { get; set; }
        public string? Email { get; set; }
        public string? ExternalAssetsID { get; set; }
        public string? ExternalAssetsReference { get; set; }
        public string? ExternalID { get; set; }
        public string? ExternalReference { get; set; }
        public string? ExternalDocBase64 { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public List<StockMovementLineResponseDTO>? Lines { get; set; }
        public bool Loan { get; set; }
        public bool Repair { get; set; }
        public bool Return { get; set; }
        public SmartUserResponseDTO? SmartUser { get; set; }
        public StockZoneResponseDTO? FromStockZone {  get; set; }
        public StockZoneResponseDTO? ToStockZone { get; set; }
        public TicketResponseDTO? Ticket {  get; set; }
        public DateTime? CreatedAt { get; set; }


    }



}
