using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.Utils.Extensions;

namespace SmartClient.MinimalAPI.Core.Utils
{
    public static class CustomRouteHandlerBuilder
    {
        public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder, bool firstErrorOnly = true) where T : class
        {
            builder.AddEndpointFilter(async (invocationContext, next) =>
            {
                var argument = invocationContext.Arguments.OfType<T>().FirstOrDefault();
                var response = argument.DataAnnotationsValidate();

                if (!response.IsValid && response.Results != null)
                {
                    string? errorMessage = firstErrorOnly ?
                                           response.Results?.FirstOrDefault()?.ErrorMessage :
                                           string.Join("|", response.Results.Select(x => x.ErrorMessage));

                    return ResultExtensions.ResultFailed(errorMessage);
                }

                return await next(invocationContext);
            });

            return builder;
        }
    }
}
