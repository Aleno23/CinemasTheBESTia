using CinemasTheBESTia.Booking.Data.Context;
using CinemasTheBESTia.Bookings.Application.Core;
using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace CinemasTheBESTia.Booking.Application.Core.Tests
{

    public class PaymentServiceFake : IPaymentService
    {
        public string Pay(string user, double total)
        {
            if (user.Equals("EXCEPTION"))
                throw new Exception();
            return user;
        }
    }

    public class BookingServiceTest
    {
        private IPaymentService _paymentServiceFake;

        private BookingService _bookingService;

        private BookingDbContext _context;

        public BookingServiceTest()
        {
            _paymentServiceFake = new PaymentServiceFake();

            var options = new DbContextOptionsBuilder<BookingDbContext>()
              .UseInMemoryDatabase("MyDataBase")
              .Options;
            _context = new BookingDbContext(options);

            _bookingService = new BookingService(_context, _paymentServiceFake);
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
                FunctionId = functionId,
                MovieId = 1,
                NumberOfTickets = numberOfTickets,
                Total = 3000,
                User = user
            };
            var result = _bookingService.ProcessBooking(reserveDTO);

            Assert.True(result.MessageCode == expectedResult);
        }

    }
}
