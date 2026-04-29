using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Parking_Blazor;
using Parking_Blazor.ApiRequests.Models;
using Parking_Blazor.ApiRequests.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalStorageServices();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5115")
});

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<UserRequests>();
builder.Services.AddScoped<CarRequests>();
builder.Services.AddScoped<ParkingRequests>();
builder.Services.AddScoped<SubscriptionRequests>();
builder.Services.AddScoped<ParkingSessionRequests>();

await builder.Build().RunAsync();