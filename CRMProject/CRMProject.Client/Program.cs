using CRMProject.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddTransient<KlantenService>();
builder.Services.AddTransient<LandService>();
builder.Services.AddSingleton(
    new HttpClient { BaseAddress = new Uri("https://localhost:7098") }
     );
await builder.Build().RunAsync();
