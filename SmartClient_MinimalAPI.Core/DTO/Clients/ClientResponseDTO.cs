using SmartClient.MinimalAPI.Core.DTO.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO.Clients
{
    public class ClientResponseDTO
    {
        public int ClientID { get; set; }
        public string? ClientName { get; set; }
        public bool Active { get; set; }
        public string? NIF {  get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<PersonResponseDTO>? Persons { get; set; }
    }
}
