using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClient.MinimalAPI.Core.DTO.StockMovements.StockMovementLines;
using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClient.MinimalAPI.Core.DTO.Suppliers;
using SmartClient.MinimalAPI.Core.DTO.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.StockMovements
{
    public class StockMovementAddRequestDTO
    {
        public int StockMovementID { get; set; }
        public int StockMovementTypeID { get; set; }
        public int? ClientID { get; set; }
        public int? PartnerClientID { get; set; }
        public int? SupplierID { get; set; }
        public int SmartUserID { get; set; }
        public string? Email { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public List<StockMovementLineAddRequestDTO>? Lines { get; set; }
        public bool Loan { get; set; }
        public bool Repair { get; set; }
        public bool Return { get; set; }
        public int? FromStockZoneID { get; set; }
        public int? ToStockZoneID { get; set; }
        public int? TicketID { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
