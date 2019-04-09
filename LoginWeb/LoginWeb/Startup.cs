using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using LoginWeb.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
