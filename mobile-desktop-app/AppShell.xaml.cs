using Android.App;
using mobile_desktop_app.Helpers;
using mobile_desktop_app.ViewModels;
using mobile_desktop_app.Views;

namespace mobile_desktop_app;


public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(Loged), typeof(Loged));
		Routing.RegisterRoute(nameof(ProfilePage),typeof(ProfilePage));
		Routing.RegisterRoute(nameof(ChangePassword), typeof(ChangePassword));
		Routing.RegisterRoute(nameof(UserCreationPage), typeof(UserCreationPage));
	
	}
}
