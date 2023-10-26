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
        public string name;
        [ObservableProperty]
        public string cpf;
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string adress;
        [ObservableProperty]
        public DateTime birthDate;
        [ObservableProperty]
        public DateTime admissionDate;
        [ObservableProperty]
        public string position;
        [ObservableProperty]
        public decimal income;

        [ObservableProperty]
        public string userPhoto;

        [ObservableProperty]
        public bool succesfullyUploaded;

        [ObservableProperty]
        public bool failedOnUpload;

        [ObservableProperty]
        public bool succesfullyRegister;

        [ObservableProperty]
        public bool failedOnRegister;




        [ObservableProperty]
        IList<Department> departments = new List<Department>()
        {
            new Department(){Id=1,Name="TI",Description="TECNOLOGIA DA INFORMAÇÃO" },
            new Department(){Id=2,Name="Vendas",Description="Departamento responsável por vendas e negociações" },
            new Department(){Id=3,Name="Financeiro",Description="Departamento responsável por finanças e contabilidade" },
            new Department(){Id=1,Name="RH",Description="Departamento responsável por recursos humanos" },
        };

        [ObservableProperty]
        Department selectedDepartment;



       






        public UserCreationViewModel()
        {
            userInfo = ServiceHelper.GetService<UserInfo>();
            AuthService = ServiceHelper.GetService<AuthService>();         
        }

        [RelayCommand]
        private async Task SelectUserPhoto() {
            try
            {

                PickOptions options = new()
                {
                    PickerTitle = "Selecione a foto do usuário"
                };

                var fileResult = await FilePicker.Default.PickAsync(options);

                if(fileResult != null)
                {
                    byte[] imageByte = File.ReadAllBytes(fileResult.FullPath);

                    UserPhoto = Convert.ToBase64String(imageByte);                   
                }

                if(UserPhoto != String.Empty)
                {
                    SuccesfullyUploaded = true;
                    FailedOnUpload = false;
                }
            }
            catch (Exception)
            {
                    UserPhoto = String.Empty;               
                    SuccesfullyUploaded = true;
                    FailedOnUpload = false;
                
                throw;
            }
     
        }

        [RelayCommand]
        private async Task AddEmployee()
        {



            var response = await AuthService.AddEmployee(new Employee() {
                address = Adress,
                admissionDate = AdmissionDate,
                birthDate =BirthDate,
                cpf = Cpf,
                departmentId = SelectedDepartment.Id,
                email = Email,
                income = Income,
                name = Name,
                position = Position,
                userPhoto = UserPhoto
            });

            if (response.IsSuccessStatusCode)
            {
                SuccesfullyRegister = true;
                FailedOnRegister = false;
            }
            else
            {
                SuccesfullyRegister = false;
                FailedOnRegister = true;
            }


        }

        [RelayCommand]
        private async Task GoLoged()
        {
            await Shell.Current.GoToAsync(nameof(Loged));
        }
    }
}
