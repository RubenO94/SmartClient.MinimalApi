using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Tickets
{
    public class TicketResponseDTO
    {
        public long TicketID { get; set; }
        public string? ClientName { get; set; }
        public string? PartnerClientName { get; set; }
        public string? From { get; set; }
        public TicketType? Type { get; set; }
        public string? State { get; set; }
        public string? Subject { get; set; }
        public DateTime? Creation { get; set; }
    }

    
}
