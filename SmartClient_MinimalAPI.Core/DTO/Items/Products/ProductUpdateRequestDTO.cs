using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Items.Products
{
    public class ProductUpdateRequestDTO
    {
        public int RowID { get; set; }
        public int ProductTypeID { get; set; }
        public string? SerialNumber { get; set; }
        public double Quantity { get; set; }
        public string? ProductTypeName { get; set; }
        public string? Description { get; set; }
        public decimal PriceUn { get; set; }
        public decimal Discount { get; set; }
        public bool Warranty { get; set; }
        public int ItemID { get; set; }
        public bool Loan { get; set; }
        public string? Picture { get; set; } // assuming base64 string representation of the picture
        public int StockZoneID { get; set; }
       
    }
}
