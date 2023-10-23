using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mobile_desktop_app.Helpers;
using mobile_desktop_app.Models;
using mobile_desktop_app.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobile_desktop_app.ViewModels
{
    public partial class LogedViewModel:ObservableObject
    {
        [ObservableProperty]
        public UserInfo userInfo;

        public LogedViewModel()
        {
            userInfo = ServiceHelper.GetService<UserInfo>();
        }

        [RelayCommand]
        private async Task GoToProfilePage()
        {
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

        [RelayCommand]
        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");

        }

        [RelayCommand]
        private async Task GotoChangePassword()
        {
            await Shell.Current.GoToAsync(nameof(ChangePassword));
        }

        [RelayCommand]
        private async Task GotoUserCreation()
        {
            await Shell.Current.GoToAsync(nameof(UserCreationPage));
        }

    }
}
