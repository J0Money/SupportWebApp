using SupportWebApp.Components;
using Microsoft.Azure.Cosmos;
using SupportWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


var cosmosAccount = builder.Configuration["CosmosDb:Account"];
var cosmosKey = builder.Configuration["CosmosDb:Key"];
var cosmosDbName = builder.Configuration["CosmosDb:DatabaseName"];
var cosmosContainer = builder.Configuration["CosmosDb:ContainerName"];

builder.Services.AddSingleton(s =>
{
    var logger = s.GetRequiredService<ILogger<CosmosSupportService>>();

    var cosmosClient = new CosmosClient(cosmosAccount, cosmosKey);

    return new CosmosSupportService(
        cosmosClient,
        cosmosDbName,
        cosmosContainer);
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
