using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Refit1.Models;
using Xamarin.Forms;
using Refit1.Services;
using Newtonsoft.Json;

namespace Refit1.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<MakeUp> _makeUps;

        public ObservableCollection<MakeUp> MakeUps { get { return _makeUps; }  set { _makeUps = value; RaisePropertyChanged("MakeUps"); } }
        public ICommand GetDataCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
            GetDataCommand = new Command(async () => await RunSafe(GetData()));
        }


        async Task GetData()
        {

            var makeUpsResponse = await ApiManager.GetMakeUps("maybelline");

            if (makeUpsResponse.IsSuccessStatusCode)
            {
                var response = await makeUpsResponse.Content.ReadAsStringAsync();
                var json = await Task.Run(() => JsonConvert.DeserializeObject<List<MakeUp>>(response));
                MakeUps = new ObservableCollection<MakeUp>(json);
            }
            else
            {
                await PageDialog.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }
    }
}
