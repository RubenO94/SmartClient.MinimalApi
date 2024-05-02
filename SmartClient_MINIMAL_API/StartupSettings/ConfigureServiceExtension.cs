using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartClientMinimalApi.StartupSettings;
using SmartClientMinimalApi.Core.Domain.ApplicationContracts;
using SmartClientMinimalApi.Core.Services;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;

namespace SmartClientMinimalApi.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAppSettings, AppSettings>();

            services.AddTransient<IJwtService, JwtService>();

            services.AddScoped<ISmartClientWebService, SmartClientWebService>();


            return services;
        }
    }
}
