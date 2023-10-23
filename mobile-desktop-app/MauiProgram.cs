using CommunityToolkit.Maui;
using Material.Components.Maui.Extensions;
using Microsoft.Extensions.Logging;
using mobile_desktop_app.Models;
using mobile_desktop_app.Services;
using mobile_desktop_app.ViewModels;
using mobile_desktop_app.Views;
using Windows.Networking.NetworkOperators;

namespace mobile_desktop_app;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			}).UseMauiCommunityToolkit()
            .UseMaterialComponents(
			new List<string>
			{
                "OpenSans-Regular.ttf",
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		//Models
		builder.Services.AddSingleton<UserInfo>();

        // views
        builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<Loged>();
		builder.Services.AddTransient<ProfilePage>();
		builder.Services.AddTransient<ChangePassword>();
		builder.Services.AddTransient<UserCreationPage>();

        builder.Services.AddSingleton<AppShell>();
		// view Models
        builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<LogedViewModel>();
		builder.Services.AddTransient<ProfilePageViewModel>();
		builder.Services.AddTransient<ChangePasswordViewModel>();
		builder.Services.AddTransient<UserCreationViewModel>();


        // services
        builder.Services.AddSingleton<AuthService>();


		return builder.Build();
	}
}
