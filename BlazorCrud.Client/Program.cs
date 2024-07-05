using BlazorCrud.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorCrud.Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5237") });

builder.Services.AddScoped<IdDepartamentoService,DepartamentoService>();
builder.Services.AddScoped<IempleadoService, EmpleadoService>();
builder.Services.AddSweetAlert2();
await builder.Build().RunAsync();
