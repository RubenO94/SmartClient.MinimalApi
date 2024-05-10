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
    }
}
