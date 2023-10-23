using mobile_desktop_app.Helpers;
using mobile_desktop_app.ViewModels;

namespace mobile_desktop_app.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<ProfilePageViewModel>();
	}
}