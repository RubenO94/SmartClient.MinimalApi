using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Suppliers
{
    public static class SupplierExtensions
    {
        public static SupplierResponseDTO? ToResponseDTO(this Supplier supplier)
        {
            if (supplier.SupplierID <= 0)
            {
                return null;
            }

            return new SupplierResponseDTO
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.Name,
                Active = supplier.Active,
                NIF = supplier.NIF,
            };
        }
    }
}
