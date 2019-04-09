using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using LoginWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

/**
 * There are two IdentityServer4 interfaces we need to implement in order to customize the process of authenticating users (against a database)
 * and generating access tokens with proper claims. 
 *  “IProfileService”. 
 * By implementing the “IResourceOwnerPasswordValidator” we can exactly program how to validate user’s credentials and authenticate them.
 * */

namespace LoginWeb.Data
{
    public class ProfileService : IProfileService
    {
        private IAuthRepository _authRepository;
        public ProfileService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userID = context.Subject.Claims.FirstOrDefault().Value;
            var credentials = _authRepository.GetUserCredentials(userID);
            var claims = new List<Claim>
                     {
                           //This is the list of claims that we are going to add to the JWT Token
new Claim(JwtClaimTypes.Role, "TSWRole1"),
new Claim(JwtClaimTypes.Role, "TSWRole2"),
new Claim(JwtClaimTypes.Role, "TSWRole2")
            };
            context.IssuedClaims = claims;
            return Task.FromResult(0);
        }
        public Task IsActiveAsync(IsActiveContext context)
        {
            // here we should check if the user is active
            context.IsActive = true;
            return Task.FromResult(0);
        }
    }
}
