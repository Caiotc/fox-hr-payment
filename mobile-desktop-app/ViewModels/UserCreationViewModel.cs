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
    public partial class UserCreationViewModel:ObservableObject
    {



        private AuthService AuthService;

        [ObservableProperty]
        public UserInfo userInfo;


        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public string newPassword;




        public UserCreationViewModel()
        {
            userInfo = ServiceHelper.GetService<UserInfo>();
            AuthService = ServiceHelper.GetService<AuthService>();
    
        }

      

        [RelayCommand]
        private async Task GoLoged()
        {
            await Shell.Current.GoToAsync(nameof(Loged));
        }
    }
}
