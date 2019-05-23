using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Entities.Movies;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using CinemasTheBESTia.Web.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CinemasTheBESTia.Web.MVC.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IAPIClient _apiClient;
        private readonly IConfiguration _configuration;

        public MoviesController(IAPIClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        // GET: Movies
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var viewModel = new MoviesViewModel();
            try
            {
                var movies = await _apiClient.GetAsync<IEnumerable<Movie>>(new Uri(_configuration["Movies:BaseUrl"]));
                viewModel.Movies = movies;
            }
            catch (Exception ex)
            {

            }
            return View(viewModel);
        }


        // GET: Movies/Details        
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            Movie movie = await GetMovie(id);
            IEnumerable<CinemaFunction> functions = await GetFunctions(movie.Id);
            return View(new MovieDetailViewModel() { Movie = movie, CinemaFunctions = functions });
        }

        private async Task<Movie> GetMovie(int id)
        {
            return await _apiClient.GetAsync<Movie>(new Uri($"{_configuration["Movies:BaseUrl"]}{id}"));
        }

        private async Task<IEnumerable<CinemaFunction>> GetFunctions(int movieId)
        {
            return await _apiClient.GetAsync<IEnumerable<CinemaFunction>>(new Uri($"{_configuration["Booking:BaseUrl"]}{movieId}"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetSeats(MovieDetailViewModel movieDetailViewModel)
        {
            var seats = await _apiClient.GetAsync<int>(new Uri($"{_configuration["Booking:BaseUrl"]}ReturnSeats/{movieDetailViewModel.CinemaFunctionID}"));
            var functions = await _apiClient.GetAsync<IEnumerable<CinemaFunction>>(new Uri($"{_configuration["Booking:BaseUrl"]}{movieDetailViewModel.Movie.Id}"));
            var movie = await _apiClient.GetAsync<Movie>(new Uri($"{_configuration["Movies:BaseUrl"]}{movieDetailViewModel.Movie.Id}"));
            var model = new MovieDetailViewModel() { Movie = movie, CinemaFunctions = functions, AvailableSeats = seats, CinemaFunctionID = movieDetailViewModel.CinemaFunctionID };
            model.AvailableSeats = seats;
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckSeats(MovieDetailViewModel movieDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                var seats = await _apiClient.GetAsync<int>(new Uri($"{_configuration["Booking:BaseUrl"]}ReturnSeats/{movieDetailViewModel.CinemaFunctionID}"));
                movieDetailViewModel.AvailableSeats = seats;
                return RedirectToAction("Index", "Booking", new
                {
                    @functionId = movieDetailViewModel.CinemaFunctionID,
                    @movieId = movieDetailViewModel.Movie.Id,
                    @movieTitle = movieDetailViewModel.Movie.OriginalTitle,
                    @voteAverage = movieDetailViewModel.Movie.VoteAverage
                
                });

            }

            Movie movie = await GetMovie(movieDetailViewModel.Movie.Id);
            movieDetailViewModel.CinemaFunctions = await GetFunctions(movieDetailViewModel.Movie.Id);
            movieDetailViewModel.Movie = movie;
            return View("Details", movieDetailViewModel);

        }

    }
}