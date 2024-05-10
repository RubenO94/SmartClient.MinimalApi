using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Addresses
{
    public class AddressResponseDTO
    {
        public string? Addr1 { get; set; }
        public string? Addr2 { get; set;}
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Country {  get; set; } 
        public string? Phone {  get; set; }

    }
}
