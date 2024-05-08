using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients;
using SmartClient.MinimalAPI.Core.DTO.Items;
using SmartClient.MinimalAPI.Core.DTO.StockZones;
using SmartClient.MinimalAPI.Core.DTO.Suppliers;
using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.SerialNumbers
{
    public static class SerialNumberExtensions
    {
        public static SerialNumberInfoResponseDTO ToResponseDTO(this SerialNumberInfoResult serialNumberInfo)
        {
            return new SerialNumberInfoResponseDTO()
            {
                SerialNumber = serialNumberInfo.SerialNumber,
                Item = serialNumberInfo?.Item.ToResponseDTO(),
                Client = serialNumberInfo?.Client.ToResponseDTO(),
                PartnerClient = serialNumberInfo?.PartnerClient.ToResponseDTO(),
                Supplier = serialNumberInfo?.Supplier.ToResponseDTO(),
                StockZone = serialNumberInfo?.Location.ToResponseDTO(),
                Loan = serialNumberInfo?.Loan ?? false,
                RepairClient = serialNumberInfo?.RepairClient ?? false,
                RepairSupplier = serialNumberInfo?.RepairSupplier ?? false,
                NotInClient = serialNumberInfo?.NotInClient ?? false,
                Warranty = serialNumberInfo?.Warranty ?? false,
                WarrantyExpiration = serialNumberInfo?.WarrantyExpiration


            };
        }
    }
}
