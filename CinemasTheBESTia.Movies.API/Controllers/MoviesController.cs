using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CinemasTheBESTia.Entities.Movies;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using CinemasTheBESTia.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CinemasTheBESTia.Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMoviesService _movieService;
        private readonly IMemoryCache _cache;
        private readonly IPolicyManager _policyManager;

        public MoviesController(IMoviesService movieService, IMemoryCache memoryCache, IPolicyManager policyManager)
        {
            _movieService = movieService;
            _cache = memoryCache;
            _policyManager = policyManager;
        }

        // GET: api/Peliculas
        [HttpGet]
        public async Task<IEnumerable<Movie>> Get()
        {

            var movies = await _policyManager.GetFallbackPolicy((cancelationToken) => GetFromCache(),
                async () => await _movieService.GetMovies());

            _cache.Set("Movie", movies);

            return movies;
        }


        private Task<IEnumerable<Movie>> GetFromCache()
        {
            if (!_cache.TryGetValue("Movie", out IEnumerable<Movie> movies))
            {
                movies = new List<Movie>();
            }
            return Task.FromResult(movies);
        }

        // GET: api/Peliculas/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Movie> Get(int id)
        {
            return await _movieService.GetMovie(id);
        }

        // POST: api/Peliculas
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Peliculas/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
