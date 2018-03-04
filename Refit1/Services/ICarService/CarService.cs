using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fusillade;
using Refit1.Services.APIManager;

namespace Refit1.Services.ICarService
{
    public class CarService : ApiManager, ICarService
    {
        public IApiService<ICarAPI> _apiService;

        public CarService(IApiService<ICarAPI> apiService)
        {
            _apiService = apiService;
        }

        public async Task<HttpResponseMessage> GetCarById(int Id)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(_apiService.GetApi(Priority.UserInitiated).GetCarByID(1, "vKVTFtC_mQ4PCbEQHyVlxNCGgIiEBducq92fY06UbnfuCupvaIjtnkVwz-knaSjWtDD3Odj_Xr8O4_9foQteBSwevmhVBAsd9FJTzqrLCB5zxbx6dL3LqwW9N105j0xXKb0dv9R7DNdn-FvLYsRrR1i3DeFmmT6FO46N0LnD9AUmLACkuefFqw0RSAK-03EgTA3k4ltpGYS599iFgqTcnRMKsiw0UH0hhZUGZOUqSI5so4gPubLsxloVd1jb5Cu6ZIhvUhFOEuf3-SiSbZjBqNEj3fhaEj-qxgc1_JC2J9blhZo_pcDk9zI9wBOpM9-FeRTP4ZoJF8XUalkmW9tCWlqoMUNFdY5LnbL5taDxjq2M50PngGQeRllu3NYVZ1LIHpo9z4sFvjevupHwhzA6-RQmVLRf39JJWoM5TliVsHUlNccASO8qjuAIdMdUn6kk4GeqIdLv_aE8DQ3zPcYaHVnvt98YC3MDGIfbLq_98uw"));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetCars()
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(_apiService.GetApi(Priority.UserInitiated).GetCars());
            runningTasks.Add(task.Id, cts);
            return await task;
        }
    }
}
