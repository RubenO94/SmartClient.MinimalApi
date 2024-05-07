using SmartClient.MinimalAPI.Core.Domain.Resources;
using SmartClientMinimalApi.Core.Domain.Resources;

namespace SmartClient.MinimalApi.EndpointFilters
{
    public class ValidationFilter : IEndpointFilter
    {

        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(ILogger<ValidationFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {

            try
            {

                //Before logic
                _logger.LogInformation($"{nameof(ValidationFilter)} before logic started");

                var result = await next(context);

                // After logic

                _logger.LogInformation($"{nameof(ValidationFilter)} after logic started");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return ResultExtensions.ResultFailed(ex.Message);
            }


           
        }
    }
}
