using System.Net.Http;
using System.Threading.Tasks;

namespace Refit1.Services.APIManager
{
    public interface IApiManager
    {
        Task<HttpResponseMessage> GetMakeUps(string brand);
        //Task<HttpResponseMessage> GetCars(string token);
        //Task<HttpResponseMessage> GetCarByID(int id,string token);
        //Task<HttpResponseMessage> LoginUser(string UserName, string Password);
    }
}
