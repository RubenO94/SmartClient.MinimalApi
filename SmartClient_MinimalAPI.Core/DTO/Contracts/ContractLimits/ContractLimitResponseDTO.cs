using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Contracts.ContractLimits
{
    public class ContractLimitResponseDTO
    {
        public long LimitID { get; set; }
        public ContractLimitTypeResponseDTO?  LimitType { get; set; }
        public double OriginalValue { get; set; }
        public double CurrentValue { get; set; }
    }
}
