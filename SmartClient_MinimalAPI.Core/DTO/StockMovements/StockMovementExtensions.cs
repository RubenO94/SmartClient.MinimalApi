using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClient.MinimalAPI.Core.DTO.StockMovements.StockMovementLines;
using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClient.MinimalAPI.Core.DTO.Suppliers;
using SmartClient.MinimalAPI.Core.DTO.Tickets;

namespace SmartClient.MinimalAPI.Core.DTO.StockMovements
{
    public static class StockMovementExtensions
    {
        public static StockMovementResponseDTO ToResponseDTO(this SmartClientWS.StockMovement stockMovement)
        {
            return new StockMovementResponseDTO
            {
                StockMovementID = stockMovement.StockMovementID,
                StockMovementTypeID = stockMovement.StockMovementTypeID,
                StockMovementTypeName = stockMovement.StockMovementTypeName,
                Email = stockMovement.Email,
                InvoiceNumber = stockMovement.InvoiceNum,
                InvoiceDate = stockMovement.InvoiceDate,
                CreatedAt = stockMovement.timestamp,
                ExternalID = stockMovement.ExternalID,
                ExternalReference = stockMovement.ExternalReference,
                ExternalAssetsID = stockMovement.ExternalAssetsID,
                ExternalAssetsReference = stockMovement.ExternalAssetsReference,
                ExternalDocBase64 = stockMovement.ExternalDocBase64,
                Loan = stockMovement.Loan,
                Repair = stockMovement.Repair,
                Return = stockMovement.Return,
                Lines = stockMovement.Lines.Select(line => line.ToResponseDTO()).ToList(),
                Client = stockMovement.Client.ToResponseDTO(),
                Supplier = stockMovement.Supplier.ToResponseDTO(),
                FromStockZone = stockMovement.FromStockZone.ToResponseDTO(),
                ToStockZone = stockMovement.ToStockZone.ToResponseDTO(),
                SmartUser = stockMovement.SmartUser.ToResponseDTO(),
                Ticket = stockMovement.Ticket.ToResponseDTO(),

            };
        }
    }
}
