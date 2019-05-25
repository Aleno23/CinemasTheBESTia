using CinemasTheBESTia.Application.Movies.Core.Movies;
using CinemasTheBESTia.Entities;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using CinemasTheBESTia.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CinemasTheBESTia.Movies.API.Tests
{
    public class CustomWebApplicationFactory<TStartup>: WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {          

        }

     
        private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            Random jitterer = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrTransientHttpError()
                .OrTransientHttpStatusCode()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound
                || msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(1, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt))
                                + TimeSpan.FromMilliseconds(jitterer.Next(0, 100)));
        }



    }
}
