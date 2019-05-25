using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Entities.Movies;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using CinemasTheBESTia.Web.MVC.Models;
using CinemasTheBESTia.Web.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CinemasTheBESTia.Web.MVC.Controllers
{
    [Authorize]
    public class UserBookingController : Controller
    {

        private readonly IAPIClient _apiClient;
        private readonly IConfiguration _configuration;

        public UserBookingController(IAPIClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        // GET: UserBooking
        public async Task<ActionResult> Index()
        {
            IEnumerable<CinemaReservation> bookings = default;
            try
            {
                bookings = await _apiClient.GetAsync<IEnumerable<CinemaReservation>>
                    (new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodBooking"]}{User.FindFirst(x => x.Type == "sub").Value}"));
            }
            catch (Exception ex)
            {

            }
            return View(bookings);
        }

        // GET: UserBooking/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var booking = await _apiClient.GetAsync<CinemaReservation>
                 (new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodBookingDetail"]}{id}"));


            return View(booking);
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(int id)
        {
            var result = await _apiClient.PostAsync<bool>($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodBooking"]}", new CancelDTO { id = id });

            if (result)
            {
                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel());
        }


    }
}