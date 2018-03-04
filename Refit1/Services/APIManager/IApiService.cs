using Fusillade;

namespace Refit1.Services.APIManager
{
    public interface IApiService<T>
    {
        T GetApi(Priority priority);
    }
}
