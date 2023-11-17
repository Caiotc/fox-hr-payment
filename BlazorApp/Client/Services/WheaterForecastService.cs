using BlazorApp.Shared;
using SharedUI.Pages.WheaterData;
using System.Net.Http.Json;

namespace BlazorApp.Client.Services
{
    public class WheaterForecastService : IWheaterForecastService
    {
        private readonly HttpClient http;

        public WheaterForecastService(HttpClient http) => this.http = http;
        public Task<WeatherForecast[]?> GetWheaterForecastsAsync() => http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }
}
