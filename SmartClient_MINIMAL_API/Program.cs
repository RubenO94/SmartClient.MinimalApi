using SmartClientMinimalApi.RouteGroups;
using SmartClientMinimalApi.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// ConfigureServiceExtension
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Route Groups
app.MapGroup("/authentication").AuthenticationAPI();
app.MapGroup("/tickets").TicketsAPI();
app.MapGroup("/smartUsers").SmartUsersAPI();

app.Run();
