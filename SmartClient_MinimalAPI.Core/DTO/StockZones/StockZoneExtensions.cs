using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.StockZones
{
    public static class StockZoneExtensions
    {
        public static StockZoneResponseDTO? ToResponseDTO(this StockZone stockZone)
        {
            if(stockZone.StockZoneID <= 0 ) return null;

            return new StockZoneResponseDTO
            {
                StockZoneID = stockZone.StockZoneID,
                StockZoneName = stockZone.Name,
                Repair = stockZone.Repair,
                MinimumStockManaged = stockZone.MinimumStockManaged,
                GenerateShippingGuide = stockZone.GenerateShippingGuide,
                ShippingGuideEmails = stockZone.ShippingGuideEmails?.Split(";").ToList(),
                MinimumStockEmails = stockZone.MinimumStockEmails?.Split(";").ToList(),  
            };
        }
    }
}
