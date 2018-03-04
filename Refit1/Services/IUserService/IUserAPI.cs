using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Refit1.Services
{
    [Headers("Content-Type: application/json")]
    public interface IUserAPI
    {
        [Post("/Token")]
        Task<HttpResponseMessage> Login([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);
    }
}
