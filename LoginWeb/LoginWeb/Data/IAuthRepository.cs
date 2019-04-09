using LoginWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb.Data
{
    public interface IAuthRepository
    {
        Task<RootObject> ValidateUserAsync(string username, string password);
        Task<string> GetUserCredentials(string username);
    }
}
