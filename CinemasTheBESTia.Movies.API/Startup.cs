using CinemasTheBESTia.Application.Movies.Core.Movies;
using CinemasTheBESTia.Entities;
using CinemasTheBESTia.Entities.Movies;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using CinemasTheBESTia.Utilities.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CinemasTheBESTia.Movies.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAPIClient, ApiClient>();
            services.AddTransient<IMoviesService, MoviesService>();
            services.AddTransient<IPolicyManager, PolicyManager>();
            services.AddTransient(x => Configuration.GetSection("Movies").Get<MovieSettings>());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpClient<IAPIClient, ApiClient>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
            .AddPolicyHandler(GetRetryPolicy());
            


            services.AddMemoryCache();

        }


        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            Random jitterer = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrTransientHttpError()
                .OrTransientHttpStatusCode()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound
                || msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(3, retryAttempt => 
                                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                + TimeSpan.FromMilliseconds(jitterer.Next(0, 100)));

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
                app.UseHsts();
            }
            // app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Movies}/{action=Get}/{id?}");
            });
        }
    }
}
