using BlazorApp.Shared;
using SharedUI.Pages.WheaterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Maui_app.Services
{
    internal class WheaterForecastMaui:IWheaterForecastService
    {
        private readonly HttpClient http;

        public WheaterForecastMaui(HttpClient http) => this.http = http;
        public Task<WeatherForecast[]?> GetWheaterForecastsAsync() => http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }
}
