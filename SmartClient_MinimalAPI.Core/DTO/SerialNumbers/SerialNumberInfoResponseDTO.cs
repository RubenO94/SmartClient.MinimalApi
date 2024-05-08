using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients;
using SmartClient.MinimalAPI.Core.DTO.Items;
using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClient.MinimalAPI.Core.DTO.Suppliers;
using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.SerialNumbers
{
    public class SerialNumberInfoResponseDTO
    {
        public string? SerialNumber { get; set; }
        public ItemResponseDTO? Item { get; set; }
        public ClientResponseDTO? Client { get; set; }
        public PartnerClientResponseDTO? PartnerClient {  get; set; }
        public SupplierResponseDTO? Supplier { get; set; }
        public StockZoneResponseDTO? StockZone { get; set; }
        public bool Loan { get; set; }
        public bool NotInClient { get; set; }
        public bool RepairClient { get; set;}
        public bool RepairSupplier { get; set;}
        public bool Warranty {  get; set; }
        public DateTime? WarrantyExpiration { get; set; }
    }
}
