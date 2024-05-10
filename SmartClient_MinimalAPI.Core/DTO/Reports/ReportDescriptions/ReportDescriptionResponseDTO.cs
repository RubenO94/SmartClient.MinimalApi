using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Reports.ReportDescriptions
{
    public class ReportDescriptionResponseDTO
    {
        public long RowID { get; set; }
        public int UserID { get; set; }
        public string? Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public decimal Total { get; set; }
        public bool Bill { get; set; }
    }
}
