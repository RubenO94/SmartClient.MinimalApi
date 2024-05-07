using SmartClient.MinimalApi.EndpointFilters;
using SmartClientMinimalApi.RouteGroups;
using SmartClientMinimalApi.StartupExtensions;
using Serilog;
using SmartClient.MinimalApi.RouteGroups;
using Asp.Versioning.ApiExplorer;
using SmartClient.MinimalApi.StartupSettings;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

// ConfigureServiceExtension
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

var app = builder.Build();

// Api Versions
var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new Asp.Versioning.ApiVersion(1))
    .HasApiVersion(new Asp.Versioning.ApiVersion(2))
    .ReportApiVersions()
    .Build();

// Set Api versions
var versionedGroups = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersionSet);

var apiVersion1 = versionedGroups.MapToApiVersion(1);
apiVersion1.AddEndpointFilter<ValidationFilter>();
// Route Groups
apiVersion1.MapGroup("authentication").AuthenticationAPI();
apiVersion1.MapGroup("tickets").TicketsAPI().RequireAuthorization();
apiVersion1.MapGroup("stockMovements").StockMovementsAPI().RequireAuthorization();
apiVersion1.MapGroup("smartUsers").SmartUsersAPI().RequireAuthorization();
apiVersion1.MapGroup("schedule").ScheduleAPI();



app.UseOutputCache();
app.UseResponseCaching();

app.UseHttpLogging();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

        foreach (var description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();

            options.SwaggerEndpoint(url, name);
        }

    });
}



app.UseAuthentication();
app.UseAuthorization();

app.Run();
