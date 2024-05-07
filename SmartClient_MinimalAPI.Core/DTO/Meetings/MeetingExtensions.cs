using SmartClient.MinimalAPI.Core.DTO.Clients;
using SmartClient.MinimalAPI.Core.DTO.Clients.PartnerClients;
using SmartClient.MinimalAPI.Core.DTO.SmartUsers;
using SmartClient.MinimalAPI.Core.DTO.Tickets;
using SmartClientWS;

namespace SmartClient.MinimalAPI.Core.DTO.Meetings
{
    public static class MeetingExtensions
    {
        public static MeetingResponseDTO ToResponseDTO(this Meeting meeting)
        {
            return new MeetingResponseDTO()
            {
                MeetingID = meeting.MeetingID,
                Title = meeting.Title,
                Description = meeting.Description,
                InternalDescription = meeting.InternalDescription,
                Location = meeting.Location,
                IsAllDay = meeting.IsAllDay,
                StartTime = meeting.Start,
                EndTime = meeting.End,
                State = meeting.State?.ToResponseDTO(),
                Client = meeting.EventClient?.ToResponseDTO(),
                PartnerClient = meeting.EventPartnerClient?.ToResponseDTO(),
                Ticket = meeting.EventTicket?.ToResponseDTO(),
                SmartUsers = meeting.SmartUsers?.Select(su => su.ToResponseDTO()).ToList()
            };
        }

        public static MeetingStateResponseDTO ToResponseDTO(this MeetingState meetingState)
        {
            return new MeetingStateResponseDTO()
            {
                MeetingStateID = meetingState.ID,
                Name = meetingState.Name,
                Abbrev = meetingState.Abbrev
            };
        }
    }
}
