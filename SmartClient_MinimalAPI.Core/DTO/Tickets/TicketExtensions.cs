using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.Tickets
{
    public static class TicketExtensions
    {
        public static TicketResponseDTO? ToResponseDTO(this Ticket ticket)
        {
            if (ticket == null || ticket.TicketID <= 0) return null;

            return new TicketResponseDTO
            {
                ClientName = ticket?.Client?.Name,
                PartnerClientName = ticket?.PartnerClient?.Name,
                From = ticket?.FromEmail,
                State = ticket?.State,
                Subject = ticket?.Subject,
                Type = ticket?.TicketType,
                Creation = ticket?.Creation ?? default,
                TicketID = ticket?.TicketID ?? 0L
            };
        }
    }
}
