using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemasTheBESTia.Entities.Movies;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemasTheBESTia.Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMoviesService _movieService;

        public MoviesController(IMoviesService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Peliculas
        [HttpGet]
        public async Task<IEnumerable<Movie>> Get()
        {
            return await _movieService.GetMovies();
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
