using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartClientMinimalApi.StartupSettings;
using SmartClientMinimalApi.Core.Domain.ApplicationContracts;
using SmartClientMinimalApi.Core.Services;
using SmartClientMinimalApi.Core.ServicesContracts;
using SmartClientWS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace SmartClientMinimalApi.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAppSettings, AppSettings>();

            services.AddTransient<IJwtService, JwtService>();

            services.AddScoped<ISmartClientWebService, SmartClientWebService>();

            services.AddTransient<ILogger>(p =>
            {
                var loggerFactory = p.GetRequiredService<ILoggerFactory>();
                return loggerFactory.CreateLogger("Serilog");
            });

            #region OpenApi
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
                
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "SmartClient Web API", Version = "1.0" });

            }); //generates OpenAPI specification
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


            return services;
        }
    }
}
