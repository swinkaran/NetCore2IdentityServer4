using IdentityServer4.Models;
using IdentityServer4.Validation;
using LoginWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * There are two IdentityServer4 interfaces we need to implement in order to customize the process of authenticating users (against a database)
 * and generating access tokens with proper claims [This is only for the Resource Owner Password Credentials Flow][OAuth2 only]. 
 * The first one is “IResourceOwnerPasswordValidator” and the second is “IProfileService”. 
 * By implementing the “IResourceOwnerPasswordValidator” we can exactly program how to validate user’s credentials and authenticate them.
 * 
 * This is the only flows that allows in-application login screen, that means that the web client application uses his own login screen. 
 * It was added to OAuth2 for legacy reasons. It should be used just for trusted application and because it doesn´t belog to the OpenID standar it doesn´t provided you with an identity_token
 * */
namespace LoginWeb.Data
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthRepository _authRep;
        public ResourceOwnerPasswordValidator(IAuthRepository authRep)
        {
            _authRep = authRep; //Dependency inyection to call the methods in the ValidateAsync Method.
        }
        /**
         * The “ValidateAsync” method in the  code simply uses the “AuthRepository” to search the Users in the webservice verify their password.
         * */
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var task = _authRep.ValidateUserAsync(context.UserName, context.Password);
            task.Wait(); // Wait for the threat to finish
            RootObject ro = task.Result; // RootObject is a innerClass in the ValidateUser.cs
            if (ro != null && ro.ValidateUser.Result == 0) // if the user is valid
            {
                context.Result = new GrantValidationResult(ro.ValidateUser.UserID.ToString(), "password", null, "local", null); //this will set the userID that we well use to get the claims in the ProfileService.cs
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The user or password are incorrect", null);
            }
            return Task.FromResult(context.Result);
        }
    }
}