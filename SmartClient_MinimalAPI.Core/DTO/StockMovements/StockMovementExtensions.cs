using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClient.MinimalAPI.Core.DTO.StockMovements.StockMovementLines;
using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClient.MinimalAPI.Core.DTO.Suppliers;
using SmartClient.MinimalAPI.Core.DTO.Tickets;
using SmartClientWS;

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

        public static StockMovement ToStockMovement(this StockMovementAddRequestDTO request)
        {
            return new StockMovement()
            {
                StockMovementID = request.StockMovementID,
                StockMovementTypeID = request.StockMovementTypeID,
                Supplier = new Supplier() { SupplierID = request.SupplierID.HasValue ? request.SupplierID.Value : default },
                Client = new Client() { ID = request.ClientID.HasValue ? request.ClientID.Value : default },
                PartnerClient = new PartnerClient() { PartnerClientID = request.PartnerClientID.HasValue ? request.PartnerClientID.Value : default},
                FromStockZone = new StockZone() { StockZoneID = request.FromStockZoneID.HasValue ? request.FromStockZoneID.Value : default },
                ToStockZone = new StockZone() { StockZoneID = request.ToStockZoneID.HasValue ? request.ToStockZoneID.Value : default },
                Ticket = new Ticket() { TicketID = request.TicketID.HasValue? request.TicketID.Value : default },
                SmartUser = new SmartUser() { UserID = request.SmartUserID},
                InvoiceNum = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                Email = request.Email,
                Loan = request.Loan,
                Repair = request.Repair,
                Return = request.Return,
                timestamp = request.CreatedAt.HasValue ? request.CreatedAt.Value : default, 
                Lines = request.Lines?.Select(line  => line.ToStockMovementLine()).ToList()
            };
        }
    }
}
