using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Client.Services;
using ListGenerator.Shared.Models;
using ListGenerator.Client.Models;
using ListGenerator.Client.Interfaces;
using ListGenerator.Client.Builders;
using AutoMapper;

namespace ListGenerator.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("ListGenerator.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


            builder.Services.AddTransient<IJsonHelper, JsonHelper>();
            builder.Services.AddTransient<IItemService, ItemService>();
            builder.Services.AddTransient<IReplenishmentService, ReplenishmentService>();
            builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IApiClient, ApiClient>();
            builder.Services.AddTransient<IItemBuilder, ItemBuilder>();
            builder.Services.AddTransient<IReplenishmentBuilder, ReplenishmentBuilder>();
            builder.Services.AddTransient<ITableService, TableService>();

            builder.Services.AddAutoMapper(typeof(Program));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ListGenerator.ServerAPI"));

            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
