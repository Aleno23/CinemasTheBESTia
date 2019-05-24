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

    public class PaymentServiceFakeOK : IPaymentService
    {
        public string Pay(string user, double total)
        {
            if (user.Equals("EXCEPTION"))
                throw new Exception();
            return user;
        }
    }



    public class CinemaServiceTest
    {

        private IPaymentService _paymentServiceFake;

        private BookingDbContext _context;

        private CinemaService _cinemaService;

        private CinemaFunctionsSettings _cinemaFunctionsSettings;

        public CinemaServiceTest()
        {
            _paymentServiceFake = new PaymentServiceFakeOK();

            var options = new DbContextOptionsBuilder<BookingDbContext>()
               .UseInMemoryDatabase("MyDataBase")
               .Options;
            _context = new BookingDbContext(options);
            _cinemaFunctionsSettings = new CinemaFunctionsSettings()
            {
                BasePrice = 1000,
                SeatsNumber = 10
            };

            _cinemaService = new CinemaService(_context, _cinemaFunctionsSettings, _paymentServiceFake);
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


        [Theory]
        [InlineData("OK", 1001, 5, 10, -1)]
        [InlineData("OK", 2001, 10, 5, 1)]
        [InlineData("FAIL", 2002, 10, 5, -2)]
        [InlineData("EXCEPTION", 2003, 10, 5, -3)]
        public void WhenProcessReserveThenReturnExpectedResult(string user, int functionId, int availableSeats, int numberOfTickets, int expectedResult)
        {
            _context.CinemaFunctions.Add(new CinemaFunction() { CinemaFuctionId = functionId, BasePrice = 1000, AvailableSeats = availableSeats, MovieId = 1 });
            _context.SaveChanges();

            var reserveDTO = new ReserveDTO
            {
                functionId = functionId,
                MovieId = 1,
                NumberOfTickets = numberOfTickets,
                Total = 3000,
                User = user
            };
            var result = _cinemaService.ProcessReserve(reserveDTO);

            Assert.True(result.MessageCode == expectedResult);
        }
    }
}
