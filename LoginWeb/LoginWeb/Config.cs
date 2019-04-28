using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb
{
    public class Config
    {
        //Defining the API that will have access to the IdentityServer
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API"),
                new ApiResource("tswTools", "Tools API")
            };
        }

        //Defining the client
        /**
        * The next step is to define a client that can access this API.
For this scenario, the client will not have an interactive user and will authenticate using the so-called client secret with IdentityServer
        **/
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    // secret for authentication
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    // scopes that client has access to
                    AllowedScopes = { "api1", "tswTools" }
                },

                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret ("secret".Sha256())
                    },
                    AllowedScopes = { "api1","tswTools" }
                }
            };
        }

        // The users that can be authenticated.
        public static List<TestUser> GetUser()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },

                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password="password"
                }
            };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "Your role(s)", new List< string>(){ "role" }) //We add a new claim to ask for when Conset page is shown
			};
        }
    }
}
