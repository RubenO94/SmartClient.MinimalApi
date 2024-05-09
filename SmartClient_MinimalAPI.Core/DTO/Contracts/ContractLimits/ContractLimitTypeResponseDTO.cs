using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Contracts.ContractLimits
{
    public class ContractLimitTypeResponseDTO
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public double Value {  get; set; } 
    }
}
