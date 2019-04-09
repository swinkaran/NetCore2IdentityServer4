using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using LoginWeb.Data;
using LoginWeb.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LoginWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddJsonFormatters(); // Add mvc and able to wok with json

            //dependency injection
            services.AddScoped<IAuthRepository, AuthRepository>(); // Adds the AuthRepository
            services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>(); // Adds the IdentityServer4 interface to customize the process of authentication
            services.AddScoped<IProfileService, ProfileService>(); // Adds the IdentityServer4 interface to customize the process of getting the claims.
            services.AddIdentityServer()
                  .AddDeveloperSigningCredential()
                  .AddInMemoryApiResources(Config.GetApiResources()) //Adds the APIResouces from the Config.cs 
                  .AddInMemoryClients(Config.GetClients()); //Adds the Clients from the Config.cs
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
                        if (error != null)
                        {
                            string jsonError = JsonConvert.SerializeObject(Utilities.GetError(123, (int)HttpStatusCode.InternalServerError, "Error", "Error description")); // this creates a new Error object. It needs the Error.cs
                            context.Response.ContentType = "application / json; charset = utf - 8";
                            context.Response.AddApplicationError(jsonError); // this adds to the headers the error message. It needs the Extension.cs
                            await context.Response.WriteAsync(jsonError);
                        }
                    });
                });// this will add the global exception handle for production evironment.
            }
            app.UseIdentityServer(); // this will add the IdentityServer
            app.UseMvc();
        }
    }
}
