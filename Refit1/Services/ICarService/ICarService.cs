using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Refit1.Services.ICarService
{
    public interface ICarService
    {
        Task<HttpResponseMessage> GetCars();
        Task<HttpResponseMessage> GetCarById(int Id);
    }
}
