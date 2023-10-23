using mobile_desktop_app.Helpers;
using mobile_desktop_app.Models;
using mobile_desktop_app.ViewModels;

namespace mobile_desktop_app.Views;

public partial class LoginPage : ContentPage
{
	public readonly LoginViewModel _loginViewModel;
    public LoginPage()
	{
		_loginViewModel = ServiceHelper.GetService<LoginViewModel>();
		InitializeComponent();
		BindingContext = _loginViewModel;		
	}
}