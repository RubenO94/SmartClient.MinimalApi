using SmartClientMinimalApi.Core.Domain.ApplicationContracts;

namespace SmartClientMinimalApi.StartupSettings
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SmartClientWSBaseURL => _configuration["ApiSettings:SmartClientWSBaseURL"] ?? "";
        public int JwtExpirationMinutes => Convert.ToInt32(_configuration["Jwt:JwtExpirationMinutes"]);

        public string JwtIssuer => _configuration["Jwt:JwtIssuer"] ?? "";

        public string JwtAudience => _configuration["Jwt:JwtAudience"] ?? "";

        public string JwtKey => _configuration["Jwt:JwtKey"] ?? "";
    }
}
