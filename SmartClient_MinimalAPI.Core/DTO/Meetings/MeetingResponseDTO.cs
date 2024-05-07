using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClient.MinimalAPI.Core.DTO.Tickets;

namespace SmartClient.MinimalAPI.Core.DTO.Meetings
{
    public class MeetingResponseDTO
    {
        public int MeetingID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? InternalDescription { get; set; }
        public string? Location { get; set; }
        public bool IsAllDay { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public MeetingStateResponseDTO? State { get; set; }
        public ClientResponseDTO? Client { get; set; }
        public PartnerClientResponseDTO? PartnerClient { get; set; }
        public TicketResponseDTO? Ticket { get; set; }
        public List<SmartUserResponseDTO>? SmartUsers { get; set; }
    }

    public class MeetingStateResponseDTO
    {
        public int MeetingStateID { get; set; }
        public string? Name { get; set; }
        public string? Abbrev { get; set; }
    }
}
