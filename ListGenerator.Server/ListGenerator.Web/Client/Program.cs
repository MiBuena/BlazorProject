using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using ListGenerator.Web.Client.Services;
using ListGenerator.Web.Shared.Interfaces;
using ListGenerator.Web.Shared.Models;
using ListGenerator.Web.Client.Interfaces;
using ListGenerator.Web.Client.Models;
using ListGenerator.Web.Client.Builders;

namespace ListGenerator.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("ListGenerator.Web.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();



            builder.Services.AddTransient<IJsonHelper, JsonHelper>();
            builder.Services.AddTransient<IItemService, ItemService>();
            builder.Services.AddTransient<IReplenishmentService, ReplenishmentService>();
            builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IApiClient, ApiClient>();
            builder.Services.AddTransient<IItemBuilder, ItemBuilder>();


            builder.Services.AddAutoMapper(typeof(Program));


            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ListGenerator.Web.ServerAPI"));

            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
