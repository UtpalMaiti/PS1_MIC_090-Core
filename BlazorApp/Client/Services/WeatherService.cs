namespace BlazorApp.Client.Services
{
    public class WeatherService : IWeatherService
    {
        public async Task InitializeWeatherAsync()
        {
          await Task.FromResult(true);
        }

         async Task IWeatherService.InitializeWeatherAsync()
        {
            await Task.FromResult(true);
        }
    }
}
