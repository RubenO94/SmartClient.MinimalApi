using SmartClient.MinimalAPI.Core.DTO.Items;
using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
