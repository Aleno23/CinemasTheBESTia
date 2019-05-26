using CinemasTheBESTia.Booking.Data.Context;
using CinemasTheBESTia.Bookings.Application.Core;
using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CinemasTheBESTia.Booking.Application.Core.Tests
{

    public class CinemaServiceTest
    {
        private BookingDbContext _context;

        private CinemaService _cinemaService;


        private CinemaFunctionsSettings _cinemaFunctionsSettings;

        public CinemaServiceTest()
        {

            var options = new DbContextOptionsBuilder<BookingDbContext>()
               .UseInMemoryDatabase("MyDataBase")
               .Options;
            _context = new BookingDbContext(options);
            _cinemaFunctionsSettings = new CinemaFunctionsSettings()
            {
                BasePrice = 1000,
                SeatsNumber = 10
            };

            _cinemaService = new CinemaService(_context, _cinemaFunctionsSettings);
        }

        [Fact]
        public async Task WhenGetThenSeedDataBase()
        {
            var result = await _cinemaService.GetFuctions(1);
            Assert.True(result.Any());
        }


        [Fact]
        public void WhenGetSeatsThenReturnNumberOfSeats()
        {
            _context.CinemaFunctions.Add(new CinemaFunction() { CinemaFuctionId = 1000, BasePrice = 1000, AvailableSeats = 5, MovieId = 1 });
            _context.SaveChanges();

            var result = _cinemaService.GetSeats(1000);
            Assert.True(result == 5);
        }

    }
}
