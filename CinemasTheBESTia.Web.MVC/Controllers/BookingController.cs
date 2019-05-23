using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using CinemasTheBESTia.Web.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Web.MVC.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {

        private readonly IAPIClient _apiClient;
        private readonly IConfiguration _configuration;

        public BookingController(IAPIClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Index(int functionId, int movieId, string movieTitle, double voteAverage, double basePrice)
        {
            var seats = await GetAvailablesSeats(functionId);

            var viewModel = new ReserverViewModel
            {
                MovieId = movieId,
                AvailableSeats = seats,
                OriginalMovieTitle = movieTitle,
                VoteAverage = voteAverage,
                CinemaFunctionId = functionId,
                PricePerTicket = basePrice * voteAverage / 10
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reserver(ReserverViewModel reserverViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _apiClient.PostAsync<ReserveResultDTO>($"{_configuration["Booking:BaseUrl"]}",
                    new
                    {
                        functionId = reserverViewModel.CinemaFunctionId,
                        movieId = reserverViewModel.MovieId,
                        numberOfTickets = reserverViewModel.NumberOfTickets,
                        user = User.FindFirst(x => x.Type == "sub").Value,
                    });

                switch (result.MessageCode)
                {
                    case 1:
                        break;
                    case -1:
                        ModelState.AddModelError("Error", "There are not seats available");
                        reserverViewModel.AvailableSeats = result.AvailableSeats;
                        break;
                    case -2:
                        ModelState.AddModelError("Error", "Payment service error");
                        break;
                    case -3:
                        ModelState.AddModelError("Error", "Please try again");
                        break;
                    default:
                        break;
                }         

            }
            return View("Index", reserverViewModel);
        }

        private async Task<int> GetAvailablesSeats(int cinemaFunctionId)
        {
            return await _apiClient.GetAsync<int>(new Uri($"{_configuration["Booking:BaseUrl"]}ReturnSeats/{cinemaFunctionId}"));
        }
    }
}
