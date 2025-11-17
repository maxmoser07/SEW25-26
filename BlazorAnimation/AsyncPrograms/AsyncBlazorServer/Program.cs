using AsyncBlazorServer.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();  // <-- REQUIRED for Blazor Server

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Map Razor components and enable interactivity
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();  // <-- REQUIRED

app.Run();