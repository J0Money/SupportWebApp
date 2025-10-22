using SupportWebApp.Components;
using Microsoft.Azure.Cosmos;
using SupportWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSingleton(s =>
{
    var cosmosClient = new CosmosClient(
        builder.Configuration["CosmosDb:Account"],
        builder.Configuration["CosmosDb:Key"]);

    var logger = s.GetRequiredService<ILogger<CosmosSupportService>>();

    return new CosmosSupportService(
        cosmosClient,
        builder.Configuration["CosmosDb:DatabaseName"],
        builder.Configuration["CosmosDb:ContainerName"]);
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
