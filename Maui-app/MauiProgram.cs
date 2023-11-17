using Maui_app.Services;
using Microsoft.Extensions.Logging;
using SharedUI.Data;
using SharedUI.Pages.WheaterData;
using SharedUI.Services;

namespace Maui_app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
           

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7278") });
            builder.Services.AddSingleton<IWheaterForecastService,WheaterForecastMaui>();
            builder.Services.AddSingleton<LogedUser>();
            builder.Services.AddSingleton<AppState>();

            builder.Services.AddTransient<ApiHelper>();
            builder.Services.AddTransient<PDFGenerator>();


            return builder.Build();
        }
    }
}