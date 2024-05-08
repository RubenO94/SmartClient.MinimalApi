using SmartClientMinimalApi.StartupSettings;
using SmartClientMinimalApi.Core.Domain.ApplicationContracts;
using SmartClientMinimalApi.Core.Services;
using SmartClientMinimalApi.Core.ServicesContracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Asp.Versioning;
using SmartClient.MinimalApi.StartupSettings;
using SmartClient.MinimalApi.StartupSettings.RouteConstraints;

namespace SmartClientMinimalApi.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<ConfigureSwaggerGenOptions>();
            services.Configure<RouteOptions>(options =>
         options.ConstraintMap.Add("serialNumber", typeof(SerialNumberRouteConstraint)));


            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddScoped<ISmartClientWebService, SmartClientWebService>();

            #region OpenApi
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            #endregion



            #region Loggers

            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            #endregion


            #region Authentication & Authorization

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder
                    .WithOrigins("*")
                    .WithHeaders("Authorization", "origin", "accept", "content-type")
                    .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
                    ;
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:JwtAudience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:JwtIssuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:JwtKey"]!))
                    };
                });
            services.AddAuthorizationBuilder().AddPolicy("admin", p => p.RequireClaim(ClaimTypes.Role, "Admin"));

            #endregion

            #region API Versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
                .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            #endregion

            #region Caching
            services.AddResponseCaching();
            services.AddOutputCache(options =>
            {
                options.AddBasePolicy(OutputCacheWithAuthPolicy.Instance);
            });
            #endregion

            return services;
        }
    }
}
