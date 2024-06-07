using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.Items.Products
{
    public static class ProductExtensions
    {
        public static ProductResponseDTO ToResponseDTO(this Product product)
        {
            return new ProductResponseDTO()
            {
                Description = product.Description,
                Discount = product.Discount,
                ItemReference = product.ItemReference.ToResponseDTO(),
                Loan = product.Loan,
                Location = product.Location.ToResponseDTO(),
                Picture = product.Picture,
                PriceUn = product.PriceUn,
                Quantity = product.Quantity,
                RowID = product.RowID,
                SerialNumber = product.SerialNumber,
                Total = product.Total,
                UserID = product.UserID,
                Warranty = product.Warranty,


            };
        }

        public static Product ToProductEntity(this ProductUpdateRequestDTO dto)
        {
            return new Product
            {
                RowID = dto.RowID,
                Description = new ProductType() { ID = dto.ProductTypeID, Name = dto.ProductTypeName},
                SerialNumber = dto.SerialNumber,
                Quantity = dto.Quantity,
                PriceUn = dto.PriceUn,
                Discount = dto.Discount,
                Warranty = dto.Warranty,
                ItemID = dto.ItemID,
                Loan = dto.Loan,
                Location = new StockZone() {  StockZoneID  = dto.StockZoneID },
                Picture = dto.Picture,
                ProductDescription = dto.Description
            };
        }
    }
}
