
using BlazorApp.Shared;

namespace SharedUI.Pages.WheaterData
{
    public interface IWheaterForecastService
    {

        Task<WeatherForecast[]?> GetWheaterForecastsAsync();
    }
}
