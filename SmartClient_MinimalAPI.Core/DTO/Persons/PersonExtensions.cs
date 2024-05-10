using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.Persons
{
    public static class PersonExtensions
    {
        public static PersonResponseDTO ToResponseDTO(this Person person)
        {
            return new PersonResponseDTO()
            {
                ClientID = person.ClientID,
                Contact = person.Contact,
                Email = person.Email,
                HasLogin = person.HasLogin,
                IsClientMaster = person.ClientMaster,
                PartnerClientID = person.PartnerClientID,
                PersonID = person.PersonID,
                PersonName = person.Name
            };
        }
    }
}
