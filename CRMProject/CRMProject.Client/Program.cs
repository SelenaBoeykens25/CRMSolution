using CRMProject.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Blazored.SessionStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddTransient<KlantenService>();
builder.Services.AddTransient<LandService>();
builder.Services.AddTransient<FactuurService>();
builder.Services.AddTransient<AccountService>();
builder.Services.AddScoped<AuthenticationNotificationService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<Microsoft.AspNetCore.Components.NavigationManager>();
    var apiBaseUrl = navigationManager.BaseUri.Contains("localhost")
        ? "https://localhost:7098"
        : "http://crmportfolioapi.runasp.net";
    return new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
});
await builder.Build().RunAsync();
