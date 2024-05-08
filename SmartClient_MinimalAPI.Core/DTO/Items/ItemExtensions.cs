using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Items
{
    public static class ItemExtensions
    {
        public static ItemResponseDTO? ToResponseDTO(this Item? item)
        {
            if(item == null || item.ItemID <= 0 ) return null;

            return new ItemResponseDTO
            {
                ItemID = item.ItemID,
                ItemName = item.Name,
                ItemTypeID = item.ItemTypeID,
                ItemTypeName = item.ItemTypeName,
                IsActive = item.IsActive,
                BasePrice = item.BasePrice,
                MinimumStockManaged = item.MinimumStockManaged,
                Reference = item.Reference,
                StockManaged = item.StockManaged
            };
        }
    }
}
