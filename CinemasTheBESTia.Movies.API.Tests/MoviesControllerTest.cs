using CinemasTheBESTia.Entities;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace CinemasTheBESTia.Movies.API.Tests
{
    public class MoviesControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory;

        public MoviesControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private MovieSettings GetMovieSettings(string serviceApiKey)
        {
            return new MovieSettings
            {
                BaseUrl = "https://api.themoviedb.org/3/discover/movie?api_key={0}&sort_by=popularity.desc&include_adult=false&include_video=false&page=1",
                ServiceApiKey = serviceApiKey,
                TokenName = "results",
                BaseUrlImage = "https://image.tmdb.org/t/p/w185_and_h278_bestv2/{0} ",
            };
        }

        [Theory]
        [InlineData("5275b13e4f70d9f311513521b5f2c89f")]
        [InlineData("9275b13e4f70d9f311513521b5f2c89f")]
        public async Task WhenGetMoviesThenResponseIsNotNull(string serviceApiKey)
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(GetMovieSettings(serviceApiKey));

                });
            }).CreateClient();

            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync("/api/movies");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            Assert.True(!string.IsNullOrWhiteSpace(stringResponse));

        }


    }
}
