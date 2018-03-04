using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit1.Models;
using Refit1.Services.ICarService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Refit1.ViewModels
{
	public class CarsPageViewModel : ViewModelBase
    {
        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars { get { return _cars; } set { _cars = value; RaisePropertyChanged("Cars"); } }
        public ICommand GetDataCommand { get; set; }
        ICarService _carService;

        public CarsPageViewModel(INavigationService navigationService, ICarService carService)
         : base(navigationService)
        {
            Title = "Main Page";
            GetDataCommand = new Command(async () => await RunSafe(GetData()));
            _carService = carService;

        }

        async Task GetData()
        {

            var makeUpsResponse = await _carService.GetCars();

            if (makeUpsResponse.IsSuccessStatusCode)
            {
                var response = await makeUpsResponse.Content.ReadAsStringAsync();
                var json = await Task.Run(() => JsonConvert.DeserializeObject<List<Car>>(response));
                Cars = new ObservableCollection<Car>(json);
            }
            else
            {
                await PageDialog.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }
    }
}
