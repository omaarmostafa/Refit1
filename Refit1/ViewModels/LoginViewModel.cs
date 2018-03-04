using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit1.Models;
using Refit1.Services.IUserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Refit1.ViewModels
{
	public class LoginViewModel : ViewModelBase
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged("UserName"); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged("Password"); }
        }

        public ICommand loginCommand { get; set; }

        private UserLogin _usr;

        public UserLogin SelectedUser
        {
            get { return _usr; }
            set { _usr = value; { RaisePropertyChanged("SelectedUser"); } }
        }
        IUserService _usrService;


        public LoginViewModel(INavigationService navigationService,IUserService usrService)
         : base(navigationService)
        {
            Title = "Main Page";
            UserName = "demo@email.com";
            Password = "123456";
            _usrService = usrService;
            loginCommand = new Command(async () => await RunSafe(LoginUser()));
        }

        async Task LoginUser()
        {

            var makeUpsResponse = await _usrService.LoginUser(UserName,Password);

            if (makeUpsResponse.IsSuccessStatusCode)
            {
                var response = await makeUpsResponse.Content.ReadAsStringAsync();
                SelectedUser = await Task.Run(() => JsonConvert.DeserializeObject<UserLogin>(response));
                await App.Current.MainPage.DisplayAlert("Successs", SelectedUser.access_token, "Ok");

            }
            else
            {
                await PageDialog.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }
    }
}
