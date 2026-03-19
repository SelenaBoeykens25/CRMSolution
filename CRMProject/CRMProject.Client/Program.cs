using CRMProject.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddTransient<KlantenService>();
builder.Services.AddTransient<LandService>();
builder.Services.AddTransient<FactuurService>();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddSingleton(
    new HttpClient { BaseAddress = new Uri("https://localhost:7098") }
     );
await builder.Build().RunAsync();
