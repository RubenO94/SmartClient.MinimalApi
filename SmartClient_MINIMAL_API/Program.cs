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

var app = builder.Build();

// Caching server-side
app.UseOutputCache();


// Api Versions
var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new Asp.Versioning.ApiVersion(1))
    .HasApiVersion(new Asp.Versioning.ApiVersion(2))
    .ReportApiVersions()
    .Build();

// Set Api versions
var versionedGroups = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersionSet);

// Route Groups
versionedGroups.MapGroup("authentication").AuthenticationV1();

versionedGroups.MapGroup("clients").ClientsAPI().RequireAuthorization();
versionedGroups.MapGroup("items").ItemsAPI().RequireAuthorization();
versionedGroups.MapGroup("tickets").TicketsAPI().RequireAuthorization();
versionedGroups.MapGroup("stockMovements").StockMovementsAPI().RequireAuthorization();
versionedGroups.MapGroup("smartUsers").SmartUsersAPI().RequireAuthorization();
versionedGroups.MapGroup("schedule").ScheduleAPI().RequireAuthorization();





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
