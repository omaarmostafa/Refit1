using Prism;
using Prism.Ioc;
using Refit1.ViewModels;
using Refit1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Plugin.FirebasePushNotification;
using Refit1.Services.APIManager;
using Refit1.Services.ICarService;
using Refit1.Services.IUserService;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Refit1
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");

                if (!string.IsNullOrEmpty(p.Identifier))
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
            };

            CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Deleted");
            };

            await NavigationService.NavigateAsync("NavigationPage/Login");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<CarsPage>();
            containerRegistry.RegisterForNavigation<CarDetailPage>();
            containerRegistry.RegisterForNavigation<Login,LoginViewModel>();
            containerRegistry.RegisterForNavigation<TestLongPress,TestLongPressViewModel>();

            containerRegistry.Register<IApiManager, ApiManager>();
            containerRegistry.Register<ICarService, CarService>();
            containerRegistry.Register<IUserService, UserService>();

            containerRegistry.Register(typeof(IApiService<>), typeof(ApiService<>));
        }
    }
}
