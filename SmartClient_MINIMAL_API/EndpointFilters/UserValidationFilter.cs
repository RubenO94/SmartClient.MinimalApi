using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClient.MinimalAPI.Core.Utils;
using SmartClientMinimalApi.Core.Domain.Resources;

namespace SmartClient.MinimalApi.EndpointFilters
{
    public class UserValidationFilter : IEndpointFilter
    {

        private readonly ILogger<UserValidationFilter> _logger;

        public UserValidationFilter(ILogger<UserValidationFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {

            try
            {
                var (isValid, userID) = AuthenticationUtils.CheckAuthenticatedUser(context.HttpContext.User);

                if (!isValid)
                {
                    _logger.LogError($"Path:{context.HttpContext.Request.Path} - Error: Tentativa de acesso com um acess_token inválido");
                    return Results.Unauthorized();
                }

                var result = await next(context);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return ResultExtensions.ResultFailed(ex.Message, true);
            }


           
        }
    }
}
