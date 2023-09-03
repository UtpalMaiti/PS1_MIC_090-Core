using BlazorApp.Server.Services;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Win32;

namespace BlazorApp.Server.Utility
{
    public static class Extension
    {
        //Register common services
        public static void ConfigureCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IDataAccess, DataAccess>();

         

        }

    }
}
