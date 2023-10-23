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
    public partial class ProfilePageViewModel:ObservableObject
    {

        [ObservableProperty]
        public UserInfo userInfo;

        public ProfilePageViewModel()
        {
            userInfo = ServiceHelper.GetService<UserInfo>();
        }


        [RelayCommand]
        private async Task GoLoged()
        {
            await Shell.Current.GoToAsync(nameof(Loged));
        }

    }
}
