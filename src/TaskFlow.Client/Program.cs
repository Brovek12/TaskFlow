using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskFlow.Client;
using TaskFlow.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5285/")
});

builder.Services.AddScoped<TokenStore>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TaskService>();

await builder.Build().RunAsync();