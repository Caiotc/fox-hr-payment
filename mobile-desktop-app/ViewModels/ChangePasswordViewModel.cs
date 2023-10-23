using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mobile_desktop_app.Helpers;
using mobile_desktop_app.Models;
using mobile_desktop_app.Services;
using mobile_desktop_app.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobile_desktop_app.ViewModels
{
    public partial class ChangePasswordViewModel:ObservableObject
    {


        private AuthService AuthService;

        [ObservableProperty]
        public UserInfo userInfo;


        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public string newPassword;

        [ObservableProperty]
        public bool succesfullyChanged;

        [ObservableProperty]
        public bool failToChange;


        public ChangePasswordViewModel()
        {
            userInfo = ServiceHelper.GetService<UserInfo>();
            AuthService = ServiceHelper.GetService<AuthService>();
            SuccesfullyChanged = false;
            FailToChange = false;
        }

        [RelayCommand]

        private async Task ChangePassword()
        {

           var response = await AuthService.ChangePasswordAsync(UserInfo.username,Password,NewPassword);

            if (response.IsSuccessStatusCode)
            {
                SuccesfullyChanged = true;
                FailToChange = false;

            }
            else
            {
                SuccesfullyChanged = false;
                FailToChange = true;
            }

            Password = String.Empty;
            NewPassword = String.Empty;
        }

        [RelayCommand]
        private async Task GoLoged()
        {
            await Shell.Current.GoToAsync(nameof(Loged));
        }

    }
}
