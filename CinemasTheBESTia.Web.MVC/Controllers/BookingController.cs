using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Entities.Movies;
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


        //[HttpPost]
        //public async Task<ActionResult> Index(int functionId, int movieId, string movieTitle, double voteAverage, double basePrice)
        //{
        //    var seats = await GetAvailablesSeats(functionId);

        //    var viewModel = new ReserverViewModel
        //    {
        //        MovieId = movieId,
        //        AvailableSeats = seats,
        //        OriginalMovieTitle = movieTitle,
        //        VoteAverage = voteAverage,
        //        CinemaFunctionId = functionId,
        //        PricePerTicket = basePrice * voteAverage / 10
        //    };

        //    return View(viewModel);
        //}

        [HttpGet]
        public async Task<ActionResult> Index(int movieId, int cinemaFunctionId)
        {

            var movie = await _apiClient.GetAsync<Movie>(new Uri($"{_configuration["Movies:BaseUrl"]}{movieId}"));
            var cinemaFunction = await _apiClient.GetAsync<CinemaFunction>(new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodFunctionById"]}{cinemaFunctionId}"));
            var viewModel = new BookingViewModel
            {
                AvailableSeats = cinemaFunction.AvailableSeats,
                PricePerTicket = cinemaFunction.BasePrice * movie.VoteAverage / 10,
                OriginalTitle = movie.OriginalTitle,
                Id = movie.Id,
                CinemaFuctionId = cinemaFunction.CinemaFuctionId
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GoToPay(BookingViewModel reserverViewModel)
        {
            if (ModelState.IsValid)
            {
                reserverViewModel.TotalPrice = (double)reserverViewModel.NumberOfTickets * reserverViewModel.PricePerTicket;
                return View("Payment", reserverViewModel);
            }
            return View("Index", reserverViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reserver(BookingViewModel reserverViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _apiClient.PostAsync<ReserveResultDTO>($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodFunctions"]}",
                    new
                    {
                        functionId = reserverViewModel.CinemaFuctionId,
                        numberOfTickets = reserverViewModel.NumberOfTickets,
                        user = User.FindFirst(x => x.Type == "sub").Value,
                        originalMovieTitle = reserverViewModel.OriginalTitle,
                        total = reserverViewModel.TotalPrice
                    });

                switch (result.MessageCode)
                {
                    case 1:
                        return RedirectToAction("Index", "Movies");
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
            return await _apiClient.GetAsync<int>(new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodSeats"]}{cinemaFunctionId}"));
        }
    }
}
