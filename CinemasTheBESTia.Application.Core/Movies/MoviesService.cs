using CinemasTheBESTia.Entities;
using CinemasTheBESTia.Entities.Movies;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Application.Core.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly MovieSettings _movieSettings;
        private readonly IAPIClient _apiClient;

        public MoviesService(IAPIClient apiClient, MovieSettings movieSettings)
        {
            _movieSettings = movieSettings;
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _apiClient.GetAsync<IEnumerable<Movie>>(new Uri(string.Format(_movieSettings.BaseUrl, _movieSettings.ServiceApiKey)), _movieSettings.TokenName);
        }
    }
}
