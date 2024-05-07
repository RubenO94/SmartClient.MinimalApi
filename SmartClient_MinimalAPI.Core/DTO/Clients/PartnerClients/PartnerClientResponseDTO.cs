using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients
{
    public class PartnerClientResponseDTO
    {
        public int PartnerClientID { get; set; }
        public int ClientID { get; set; }
        public string? PartnerClientName { get; set; }
        public bool Active { get; set; }
        public string? NIF { get; set; }

    }
}
