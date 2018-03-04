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
using Refit1.Services.ICarService;

namespace Refit1.ViewModels
{
	public class CarDetailPageViewModel : ViewModelBase
    {
        private Car _car;
        public Car SelectedCar { get { return _car; } set { _car = value; RaisePropertyChanged("SelectedCar"); } }
        public ICommand GetDataCommand { get; set; }

        ICarService _carService;

        public CarDetailPageViewModel(INavigationService navigationService,ICarService carService)
         : base(navigationService)
        {
            Title = "Main Page";
            GetDataCommand = new Command(async () => await RunSafe(GetData()));
            _carService = carService;
        }

        async Task GetData()
        {

            var makeUpsResponse = await _carService.GetCarById(1);
            if (makeUpsResponse.IsSuccessStatusCode)
            {
                var response = await makeUpsResponse.Content.ReadAsStringAsync();
                var json = await Task.Run(() => JsonConvert.DeserializeObject<Car>(response));
                SelectedCar = json;
            }
            else
            {
                await PageDialog.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }
    }
}
