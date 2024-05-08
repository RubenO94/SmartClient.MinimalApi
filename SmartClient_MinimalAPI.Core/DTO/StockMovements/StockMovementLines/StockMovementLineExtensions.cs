using SmartClient.MinimalAPI.Core.DTO.Items;
using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.StockMovements.StockMovementLines
{
    public static class StockMovementLineExtensions
    {

        public static StockMovementLineResponseDTO ToResponseDTO(this StockMovementLine line) {

            return new StockMovementLineResponseDTO
            {
                StockMovementLineID = line.StockMovementLineID,
                GenerateSerialNumbers = line.GenerateSerialNumbers,
                Quantity = line.Quantity,
                QuantityAfter = line.QuantityAfter,
                QuantityBefore  = line.QuantityBefore,
                SerialNumbers = line.SerialNumbers.ToList(),
                Item = line.Item.ToResponseDTO()

            };
        }

       public static StockMovementLine ToStockMovementLine(this StockMovementLineAddRequestDTO request)
        {
            return new StockMovementLine()
            {
               StockMovementLineID = request.StockMovementLineID,
               GenerateSerialNumbers = request.GenerateSerialNumbers,
               Item = new Item() { ItemID = request.ItemID },
               Quantity = request.Quantity, 
               QuantityAfter = request.QuantityAfter,   
               QuantityBefore = request.QuantityBefore, 
               SerialNumbers = request.SerialNumbers?.ToList(),
            };
        }
    }
}
