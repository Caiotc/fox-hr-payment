using mobile_desktop_app.Helpers;
using mobile_desktop_app.ViewModels;

namespace mobile_desktop_app.Views;

public partial class Loged : ContentPage
{
	public Loged()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<LogedViewModel>();
	}


}