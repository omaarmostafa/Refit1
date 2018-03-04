using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Fusillade;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Polly;
using Refit;

namespace Refit1.Services.APIManager
{
    public class ApiManager : IApiManager
    {
        IUserDialogs _userDialogs = UserDialogs.Instance;
        IConnectivity _connectivity = CrossConnectivity.Current;

        public bool IsConnected { get; set; }
        public bool IsReachable { get; set; }
        public Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();
        public Dictionary<string, Task<HttpResponseMessage>> taskContainer = new Dictionary<string, Task<HttpResponseMessage>>();

        public ApiManager()
        {
            IsConnected = _connectivity.IsConnected;
            _connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

            if (!e.IsConnected)
            {
                // Cancel All Running Task
                var items = runningTasks.ToList();
                foreach (var item in items)
                {
                    item.Value.Cancel();
                    runningTasks.Remove(item.Key);
                }
            }
        }

        public async Task<HttpResponseMessage> GetMakeUps(string brand)
        {
            //var cts = new CancellationTokenSource();
            //var task = RemoteRequestAsync<HttpResponseMessage>(makeUpApi.GetApi(Priority.UserInitiated).GetMakeUps(brand));
            //runningTasks.Add(task.Id, cts);
            //return await task;
            return null;
        }

        //public async Task<HttpResponseMessage> GetCars(string _accessToken = "vKVTFtC_mQ4PCbEQHyVlxNCGgIiEBducq92fY06UbnfuCupvaIjtnkVwz-knaSjWtDD3Odj_Xr8O4_9foQteBSwevmhVBAsd9FJTzqrLCB5zxbx6dL3LqwW9N105j0xXKb0dv9R7DNdn-FvLYsRrR1i3DeFmmT6FO46N0LnD9AUmLACkuefFqw0RSAK-03EgTA3k4ltpGYS599iFgqTcnRMKsiw0UH0hhZUGZOUqSI5so4gPubLsxloVd1jb5Cu6ZIhvUhFOEuf3-SiSbZjBqNEj3fhaEj-qxgc1_JC2J9blhZo_pcDk9zI9wBOpM9-FeRTP4ZoJF8XUalkmW9tCWlqoMUNFdY5LnbL5taDxjq2M50PngGQeRllu3NYVZ1LIHpo9z4sFvjevupHwhzA6-RQmVLRf39JJWoM5TliVsHUlNccASO8qjuAIdMdUn6kk4GeqIdLv_aE8DQ3zPcYaHVnvt98YC3MDGIfbLq_98uw")
        //{
        //    var cts = new CancellationTokenSource();
        //    var task = RemoteRequestAsync<HttpResponseMessage>(carApi.GetApi(Priority.UserInitiated).GetCars());
        //    runningTasks.Add(task.Id, cts);
        //    return await task;
        //}

        //public async Task<HttpResponseMessage> GetCarByID(int id, string _accessToken)
        //{
        //    var cts = new CancellationTokenSource();
        //    var task = RemoteRequestAsync<HttpResponseMessage>(carApi.GetApi(Priority.UserInitiated).GetCarByID(1, "vKVTFtC_mQ4PCbEQHyVlxNCGgIiEBducq92fY06UbnfuCupvaIjtnkVwz-knaSjWtDD3Odj_Xr8O4_9foQteBSwevmhVBAsd9FJTzqrLCB5zxbx6dL3LqwW9N105j0xXKb0dv9R7DNdn-FvLYsRrR1i3DeFmmT6FO46N0LnD9AUmLACkuefFqw0RSAK-03EgTA3k4ltpGYS599iFgqTcnRMKsiw0UH0hhZUGZOUqSI5so4gPubLsxloVd1jb5Cu6ZIhvUhFOEuf3-SiSbZjBqNEj3fhaEj-qxgc1_JC2J9blhZo_pcDk9zI9wBOpM9-FeRTP4ZoJF8XUalkmW9tCWlqoMUNFdY5LnbL5taDxjq2M50PngGQeRllu3NYVZ1LIHpo9z4sFvjevupHwhzA6-RQmVLRf39JJWoM5TliVsHUlNccASO8qjuAIdMdUn6kk4GeqIdLv_aE8DQ3zPcYaHVnvt98YC3MDGIfbLq_98uw"));
        //    runningTasks.Add(task.Id, cts);
        //    return await task;
        //}

        //public async Task<HttpResponseMessage> LoginUser(string UserName, string Password)
        //{
        //    var cts = new CancellationTokenSource();
        //    Dictionary<string, string> _credentials = new Dictionary<string, string>();
        //    _credentials.Add("userName", UserName);
        //    _credentials.Add("password", Password);
        //    _credentials.Add("grant_type", "password");
        //    var task = RemoteRequestAsync<HttpResponseMessage>(userApi.GetApi(Priority.UserInitiated).Login(_credentials));

        //    runningTasks.Add(task.Id, cts);
        //    return await task;
        //}
        protected async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
        where TData : HttpResponseMessage,
        new()
        {
            TData data = new TData();

            if (!IsConnected)
            {
                var strngResponse = "There's not a network connection";
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(strngResponse);

                _userDialogs.Toast(strngResponse, TimeSpan.FromSeconds(1));
                return data;
            }

            IsReachable = await _connectivity.IsRemoteReachable(Config.ApiHostName);

            if (!IsReachable)
            {
                var strngResponse = "There's not an internet connection";
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(strngResponse);

                _userDialogs.Toast(strngResponse, TimeSpan.FromSeconds(1));
                return data;
            }

            data = await Policy
            .Handle<WebException>()
            .Or<ApiException>()
            .Or<TaskCanceledException>()
            .WaitAndRetryAsync
            (
                retryCount: 2,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            )
            .ExecuteAsync(async () =>
            {
                var result = await task;

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //Logout the user 
                }
                runningTasks.Remove(task.Id);

                return result;
            });

            return data;
        }
    }
}
