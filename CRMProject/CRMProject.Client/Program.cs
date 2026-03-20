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
builder.Services.AddSingleton(
    new HttpClient { BaseAddress = new Uri("https://localhost:7098") }
     );
await builder.Build().RunAsync();
