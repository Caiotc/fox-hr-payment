using mobile_desktop_app.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mobile_desktop_app.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private LoginRequestModel myLoginRequestModel = new LoginRequestModel();

        public LoginRequestModel MyLoginRequestModel
        {
            get { return myLoginRequestModel; }
            set
            {
                myLoginRequestModel = value;
                OnPropertyChanged(nameof(MyLoginRequestModel));
            }
        }
        public ICommand LoginCommand { get; set; }


        public LoginViewModel()
        {
            LoginCommand = new Command(PerformLoginOperation);
        }

        private async void PerformLoginOperation(object obj)
        {
            // api operation
            //login on api

            var data = myLoginRequestModel;
            await Shell.Current.GoToAsync(state: "//");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
