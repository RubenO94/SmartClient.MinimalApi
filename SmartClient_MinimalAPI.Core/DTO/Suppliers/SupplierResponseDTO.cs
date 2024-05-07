using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Suppliers
{
    public class SupplierResponseDTO
    {
        public int SupplierID { get; set; }
        public string? SupplierName { get; set;}
        public string? NIF { get; set;}
        public bool Active { get; set; }
    }
}
