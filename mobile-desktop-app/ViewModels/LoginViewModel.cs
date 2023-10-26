using mobile_desktop_app.Models;
using mobile_desktop_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mobile_desktop_app.Helpers;
using mobile_desktop_app.Views;

namespace mobile_desktop_app.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        [ObservableProperty]
        private LoginRequestModel myLoginRequestModel = new LoginRequestModel();



        [ObservableProperty]
        public bool failedToLogin;


        

        
        private UserInfo _userInfo;


        private readonly AuthService _authService;

        public LoginViewModel(AuthService actualState)
        {
            _authService = actualState;
            _userInfo = ServiceHelper.GetService<UserInfo>();
        }

        [RelayCommand]
        private async Task PerformLoginOperation(object obj)
        {
           var data = MyLoginRequestModel;

            try
            {
                var response = await _authService.IsAuthenticatedAsync(data.UserName, data.Password);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<UserInfo>(response.Content!);

                    _userInfo.jwtBearer = "Bearer " + result.jwtBearer;
                    _userInfo.username = result.username;
                    _userInfo.userPhoto = result.userPhoto;
                    _userInfo.role = result.role;
                    _userInfo.roleId = result.roleId;
                    _userInfo.id = result.id;
                    _userInfo.employeeId = result.employeeId;
                    _userInfo.employee = result.employee;

                    await Shell.Current.GoToAsync(nameof(Loged));

                }
                else
                    FailedToLogin = true;
            }
            catch (Exception)
            {
                FailedToLogin = true;

                throw;
            }


        }

    }
}
