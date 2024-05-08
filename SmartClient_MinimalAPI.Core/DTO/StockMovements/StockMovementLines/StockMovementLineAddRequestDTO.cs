using SmartClient.MinimalAPI.Core.DTO.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.StockMovements.StockMovementLines
{
    public class StockMovementLineAddRequestDTO
    {
        public int StockMovementLineID { get; set; }
        public int ItemID { get; set; }
        public bool GenerateSerialNumbers { get; set; }
        public float Quantity { get; set; }
        public float QuantityBefore { get; set; }
        public float QuantityAfter { get; set; }
        public List<string>? SerialNumbers { get; set; }
    }
}
