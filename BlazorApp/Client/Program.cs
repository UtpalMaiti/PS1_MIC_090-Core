using BlazorApp.Client;
using BlazorApp.Client.Services;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Client
{
    public class Program
    {
        public static  IConfiguration Configuration { get; set; }
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClient("BlazorApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorApp.ServerAPI"));


            builder.Services.AddSingleton<IDataAccess, DataAccess>();
            builder.Services.AddSingleton<IWeatherService, WeatherService>();


            builder.Services.AddApiAuthorization();
            var host = builder.Build();

            Configuration = host.Configuration;
            //var weatherServiceUrl = Configuration["WeatherServiceUrl"];

            var weatherService = host.Services.GetRequiredService<IWeatherService>();
            await weatherService.InitializeWeatherAsync();

            await host.RunAsync();
        }
    }
}