using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients
{
    public static class PartnerClientExtensions
    {
        public static PartnerClientResponseDTO? ToResponseDTO(this PartnerClient partnerClient)
        {
            if (partnerClient.PartnerClientID <= 0)
            {
                return null;
            }

            return new PartnerClientResponseDTO
            {
                PartnerClientID = partnerClient.PartnerClientID,
                ClientID = partnerClient.ClientID,
                PartnerClientName = partnerClient.Name,
                Active = partnerClient.Active,
                NIF = partnerClient.NIF,
            };
        }
    }
}
