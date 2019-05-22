using CinemasTheBESTia.Entities;
using CinemasTheBESTia.Entities.Movies;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Application.Movies.Core.Movies
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
            var movies=  await _apiClient.GetAsync<IEnumerable<Movie>>(new Uri(string.Format(_movieSettings.BaseUrl, _movieSettings.ServiceApiKey)), _movieSettings.TokenName);
            movies.ToList().ForEach(x => x.FullPosterPath = string.Format(_movieSettings.BaseUrlImage, x.PosterPath));
            return movies;

        }
    }
}
