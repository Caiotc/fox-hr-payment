using CommunityToolkit.Maui.Behaviors;
using mobile_desktop_app.Helpers;
using mobile_desktop_app.ViewModels;

namespace mobile_desktop_app.Views;

public partial class UserCreationPage : ContentPage
{
	public UserCreationPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<UserCreationViewModel>();



        //var entry = new Entry
        //{
        //    Keyboard = Keyboard.Numeric
        //};

        //var validStyle = new Style(typeof(Entry));
        //validStyle.Setters.Add(new Setter
        //{
        //    Property = Entry.TextColorProperty,
        //    Value = Colors.Green
        //});

        //var invalidStyle = new Style(typeof(Entry));
        //invalidStyle.Setters.Add(new Setter
        //{
        //    Property = Entry.TextColorProperty,
        //    Value = Colors.Red
        //});

        //var numericValidationBehavior = new NumericValidationBehavior
        //{
        //    InvalidStyle = invalidStyle,
        //    ValidStyle = validStyle,
        //    Flags = ValidationFlags.ValidateOnValueChanged,
        //    MinimumValue = 1.0,
        //    MaximumValue = 100.0,
        //    MaximumDecimalPlaces = 2
        //};

        //entry.Behaviors.Add(numericValidationBehavior);

        //income = entry;

    }


}