using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.StockZones
{
    public class StockZoneResponseDTO
    {
        public int StockZoneID { get; set; }
        public string? StockZoneName { get; set; }
        public bool Repair { get; set; }
        public bool GenerateShippingGuide { get; set; }
        public bool MinimumStockManaged { get; set; }
        public List<string>? ShippingGuideEmails { get; set; }
        public List<string>? MinimumStockEmails { get; set; }
    }
}
