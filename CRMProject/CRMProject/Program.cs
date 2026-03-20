using CRMProject.Client.Services;
using CRMProject.Client.Pages;
using CRMProject.Components;
using Radzen;
using Blazored.SessionStorage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(
    new HttpClient { BaseAddress = new Uri("https://localhost:7098") }
     );
builder.Services.AddTransient<KlantenService>();
builder.Services.AddTransient<LandService>();
builder.Services.AddTransient<FactuurService>();
builder.Services.AddTransient<AccountService>();
builder.Services.AddScoped<AuthenticationNotificationService>();
builder.Services.AddBlazoredSessionStorage();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CRMProject.Client._Imports).Assembly);

app.Run();
