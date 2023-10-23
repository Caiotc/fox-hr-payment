using mobile_desktop_app.Helpers;
using mobile_desktop_app.ViewModels;

namespace mobile_desktop_app.Views;

public partial class UserCreationPage : ContentPage
{
	public UserCreationPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<UserCreationViewModel>();
	}
}