using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using Refit1.Services.APIManager;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Refit1.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        public IUserDialogs PageDialog = UserDialogs.Instance;
        public IApiManager ApiManager;
        //IApiService<IMakeUpApi> makeUpApi = new ApiService<IMakeUpApi>(Config.ApiUrl);
        //IApiService<ICarAPI> carApi = new ApiService<ICarAPI>(Config.ApiUrl);
        //IApiService<IUserAPI> userApi = new ApiService<IUserAPI>(Config.ApiUrl);


        public bool IsBusy { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            //ApiManager = new ApiManager();
        }

        public async Task RunSafe(Task task, bool ShowLoading = true, string loadinMessage = null)
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;

                if (ShowLoading) UserDialogs.Instance.ShowLoading(loadinMessage ?? "Loading");

                await task;
            }
            catch (Exception e)
            {
                IsBusy = false;
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(e.ToString());
                await App.Current.MainPage.DisplayAlert("Eror", "Check your internet connection", "Ok");

            }
            finally
            {
                IsBusy = false;
                if (ShowLoading) UserDialogs.Instance.HideLoading();
            }
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        public virtual void Destroy()
        {
            
        }
    }
}
