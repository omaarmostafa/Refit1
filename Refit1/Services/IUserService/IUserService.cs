using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Refit1.Services.IUserService
{
    public interface IUserService
    {
        Task<HttpResponseMessage> LoginUser(string UserName, string Password);
    }
}
