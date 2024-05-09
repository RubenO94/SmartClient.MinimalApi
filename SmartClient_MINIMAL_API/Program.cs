using SmartClientMinimalApi.StartupExtensions;
using Serilog;
using Asp.Versioning.ApiExplorer;
using SmartClient.MinimalApi.RouteGroups.v1;
using SmartClientMinimalApi.RouteGroups;
using SmartClient.MinimalApi.RouteGroups;

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
versionedGroups.MapGroup("clients").ClientsV1();
versionedGroups.MapGroup("items").ItemsV1();
versionedGroups.MapGroup("reports").ReportsV1();
versionedGroups.MapGroup("schedule").ScheduleV1();
versionedGroups.MapGroup("smartUsers").SmartUsersV1();
versionedGroups.MapGroup("stockMovements").StockMovementsV1();
versionedGroups.MapGroup("stockZones").StockZonesV1();
versionedGroups.MapGroup("suppliers").SuppliersV1();
versionedGroups.MapGroup("tickets").TicketsV1();






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
