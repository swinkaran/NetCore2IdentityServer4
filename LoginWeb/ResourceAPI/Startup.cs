using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LoginWeb.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ResourceAPI
{
    public class Startup
    {
        private const string IDENTITY_SERVER_IP ="http://localhost:5000";//For test environment
        //private const string IDENTITY_SERVER_IP = “https://cesionderechos.grupovidanta.com:442“; //For production environment
        // This method gets called by the runtime. Use this method to add services to the container.

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(); //this adds a lot of functionality that we dont need is better to use AddMvcCore
            services.AddMvcCore()
                       .AddAuthorization()
                       .AddJsonFormatters();
            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(option =>
            {
                option.Authority = IDENTITY_SERVER_IP;
                option.RequireHttpsMetadata = false;
                //option.ApiSecret = “secret”;
                option.ApiName = "tswTools"; //This is the resourceAPI that we defined in the Config.cs in the LoginWeb project above. In order to work it has to be named equal.
            });
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
                            string jsonError = JsonConvert.SerializeObject(Utilities.GetError(123, (int)HttpStatusCode.InternalServerError, "Error", "error message"));
                            context.Response.ContentType = "application / json; charset = utf - 8";
                            context.Response.AddApplicationError(jsonError);
                            await context.Response.WriteAsync(jsonError);
                        }
                    });
                });// this will add the global exception handle
            }
            app.UseAuthentication(); //Adds authentication to the application that we defined in the Configuration Services.
            app.UseMvc();
        }
    }
}
