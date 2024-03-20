using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Admin.Web;
using Admin.Web.Services;
using SchoolApp.Admin.Web;
using SchoolApp.Admin.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
ServiceExstension.AddServices(builder);
// Configure HttpClient with base address from appsettings.json

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:5002") });

var env = builder.HostEnvironment;
env.IsDevelopment();

await builder.Build().RunAsync();


