using SmartClientWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClient.MinimalAPI.Core.DTO
{
    public class TicketDTO
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

    public static class TicketExtensions
    {
        public static TicketDTO ToDTO(this Ticket ticket)
        {
            return new TicketDTO
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
