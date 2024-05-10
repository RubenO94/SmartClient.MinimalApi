using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.Items.Products
{
    public class ProductResponseDTO
    {
        public int RowID { get; set; }
        public double Quantity { get; set; }
        public ProductType? Description { get; set; }
        public string? SerialNumber { get; set; }
        public decimal PriceUn { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public bool Warranty { get; set; }
        public string? Picture { get; set; }
        public int UserID { get; set; }
        public bool Loan { get; set; }
        public StockZoneResponseDTO? Location { get; set; }
        public ItemResponseDTO? ItemReference { get; set; }

        public decimal ComputeTotal()
        {
            return (decimal)Quantity * PriceUn * (1m - Discount * 0.01m);
        }
    }
}
