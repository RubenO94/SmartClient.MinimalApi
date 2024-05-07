using Microsoft.AspNetCore.Mvc;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.DTO.Meetings;
using SmartClientMinimalApi.Core.ServicesContracts;
using System.Security.Claims;

namespace SmartClient.MinimalApi.RouteGroups
{
    public static class ScheduleMapGroup
    {
        public static RouteGroupBuilder ScheduleAPI(this RouteGroupBuilder group)
        {

            group.MapGet("/", async (HttpContext context, ILoggerFactory loggerFactory, ISmartClientWebService clientWebService, [FromQuery] DateTime Start, [FromQuery] DateTime End) =>
            {
                try
                {
                    var userID = Convert.ToInt32(context.User.FindFirstValue(ClaimTypes.NameIdentifier));

                    if (userID <= 0)
                    {
                        return ResultExtensions.ResultFailed($"Utilizador com ID {userID} não é válido");
                    }

                    var response = await clientWebService.GetService().GetAllMeetingsAsync(Start, End, new List<int> { userID });

                    if(response == null)
                    {
                        throw new ArgumentException("Scheduler nulo");
                    }

                    var meetingsDTO = response.Select(mt => mt.ToResponseDTO()).ToList();

                    return meetingsDTO.ToResult();
                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger($"RouteGroups.{nameof(RouteGroupBuilder)}.{ScheduleAPI}");
                    logger.LogError($"Path:{context.Request.Path} - Error: {ex.Message}");
                    return ResultExtensions.ResultFailed(ex.Message, true);
                }

            }).CacheOutput( options =>
            {
                options.Expire(TimeSpan.FromSeconds(120));
            });

            return group;
        }
    }
}
