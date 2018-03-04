using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Refit1.Services
{
    [Headers("Content-Type: application/json")]
    public interface ICarAPI
    {
        [Get("/api/Cars")]
        Task<HttpResponseMessage> GetCars();

        [Get("/api/Cars/{id}")]
        Task<HttpResponseMessage> GetCarByID(int id,[Header("Authorization")] string token);
    }
}
