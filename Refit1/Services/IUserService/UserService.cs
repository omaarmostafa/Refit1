using Fusillade;
using Refit1.Services.APIManager;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Refit1.Services.IUserService
{
    public class UserService : ApiManager,  IUserService
    {
        public IApiService<IUserAPI> _apiService;

        public UserService(IApiService<IUserAPI> apiService)
        {
            _apiService = apiService;
        }

        public async Task<HttpResponseMessage> LoginUser(string UserName, string Password)
        {
            var cts = new CancellationTokenSource();
            Dictionary<string, string> _credentials = new Dictionary<string, string>();
            _credentials.Add("userName", UserName);
            _credentials.Add("password", Password);
            _credentials.Add("grant_type", "password");
            var task = RemoteRequestAsync<HttpResponseMessage>(_apiService.GetApi(Priority.UserInitiated).Login(_credentials));

            runningTasks.Add(task.Id, cts);
            return await task;
        }
    }
}
