using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Refit1.Models;

namespace Refit1.Services
{
    [Headers("Content-Type: application/json")]
    public interface IMakeUpApi
    {
        [Get("/api/v1/products.json?brand={brand}")]
        Task<HttpResponseMessage> GetMakeUps(string brand);

        [Post("/api/v1/addMakeUp")]
        Task<MakeUp> CreateMakeUp([Body] MakeUp makeUp, [Header("Authorization")] string token);
    }
}
