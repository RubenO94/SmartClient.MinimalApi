using SmartClient.MinimalApi.EndpointFilters;
using SmartClientMinimalApi.RouteGroups;
using SmartClientMinimalApi.StartupExtensions;
using Serilog;

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

// Route Groups
app.MapGroup("/authentication").AuthenticationAPI();
app.MapGroup("/tickets").TicketsAPI().RequireAuthorization().AddEndpointFilter<ValidationFilter>();
app.MapGroup("/smartUsers").SmartUsersAPI().RequireAuthorization();


app.UseHttpLogging();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
    options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "1.0");
});


app.UseAuthentication();
app.UseAuthorization();

app.Run();
