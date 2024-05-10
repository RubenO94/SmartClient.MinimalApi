using System.Text.Json.Serialization;

namespace SmartClient.MinimalAPI.Core.DTO.Persons
{
    public class PersonResponseDTO
    {
        public int PersonID { get; set; }
        public int ClientID { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PartnerClientID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public bool HasLogin { get; set; }
        public bool IsClientMaster { get; set; }
    }
}
