using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using LoginWeb.Helper;
using LoginWeb.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LoginWeb.Data
{
    public class AuthRepository : IAuthRepository

    {
        static HttpClient client = new HttpClient();
        readonly string TswCustomEntpoint = "http";
        readonly string TswProxyEntpoint = "http://10.2.180.10:8086/api/tswuser&#8221";

        public async Task<string> GetUserCredentials(string username)
        {
            // Return the string with the credentials
            return "";
        }

        public async Task<RootObject> ValidateUserAsync(string username, string password)
        {
            // Returns the user information.
            RootObject root = new RootObject();
            root.ValidateUser = new ValidateUser { UserName = "alice", UserID = 121212, Result = 1 };

            return root;
            //return new RootObject();
        }
    }
}
