using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Clients
{
    public static class ClientExtensions
    {
        public static ClientResponseDTO? ToResponseDTO(this Client? client)
        {
            if(client == null || client.ID <= 0)
            {
                return null;
            }

            return new ClientResponseDTO
            {
                ClientID = client.ID,
                ClientName = client.Name,
                Active = client.Active,
                NIF = client.NIF,
            };
        }
    }
}
