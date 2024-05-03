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

            //Before logic
            _logger.LogInformation($"{nameof(ValidationFilter)} before logic started");
            
            var result = await next(context);
            
            // After logic

            _logger.LogInformation($"{nameof(ValidationFilter)} after logic started");


            return result;
        }
    }
}
