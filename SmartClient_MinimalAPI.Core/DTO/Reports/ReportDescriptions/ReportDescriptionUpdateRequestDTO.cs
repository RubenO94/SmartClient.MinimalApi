using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Reports.ReportDescriptions
{
    public class ReportDescriptionUpdateRequestDTO
    {
        [Required(ErrorMessage = $"Parametro {nameof(RowID)} não pode ser vazio ou nulo")]
        public int RowID { get; set; }
        [Required(ErrorMessage = $"Parametro {nameof(Start)} não pode ser vazio ou nulo")]
        public DateTime? Start {  get; set; }
        [Required(ErrorMessage = $"Parametro {nameof(End)} não pode ser vazio ou nulo")]
        public DateTime? End { get; set; }
        public bool Invoice { get; set; }
    }
}
