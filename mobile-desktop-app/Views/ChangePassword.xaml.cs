using mobile_desktop_app.Helpers;
using mobile_desktop_app.ViewModels;

namespace mobile_desktop_app.Views;

public partial class ChangePassword : ContentPage
{
	public ChangePassword()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<ChangePasswordViewModel>();
	}
}