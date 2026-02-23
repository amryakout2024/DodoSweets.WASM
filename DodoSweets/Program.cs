using DodoSweets;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddLocalization();


var host = builder.Build();




// Get saved culture or default to English
var js = host.Services.GetRequiredService<IJSRuntime>();
var CultureName = await js.InvokeAsync<string>("localStorage.getItem", "BlazorCulture");
var culture = CultureName ?? "en-US";

var cultureInfo = new CultureInfo(culture);
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo; 

await host.RunAsync();
