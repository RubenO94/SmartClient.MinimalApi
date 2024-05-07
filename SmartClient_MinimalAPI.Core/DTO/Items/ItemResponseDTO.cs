using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Items
{
    public class ItemResponseDTO
    {
        public int ItemID { get; set; }
        public string? Reference { get; set; }
        public string? ItemName { get; set; }
        public int ItemTypeID { get; set; }
        public string? ItemTypeName { get; set; }
        public float BasePrice { get; set; }
        public bool StockManaged { get; set; }
        public bool MinimumStockManaged { get; set; }
        public bool IsActive { get; set; }
    }
}
