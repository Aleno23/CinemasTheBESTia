using CinemasTheBESTia.Booking.Data.Context;
using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Bookings.Application.Core
{
    public class CinemaService : ICinemasService
    {
        private readonly BookingDbContext _context;
        private readonly CinemaFunctionsSettings _cinemaFunctionsSettings;
        private readonly IPaymentService _paymentService;
        private static object _threadSafe = new object();

        public CinemaService(BookingDbContext context, CinemaFunctionsSettings cinemaFunctionsSettings, IPaymentService paymentService)
        {
            _context = context;
            _cinemaFunctionsSettings = cinemaFunctionsSettings;
            _paymentService = paymentService;
        }

        public async Task<IEnumerable<CinemaFunction>> GetFuctions(int movieId)
        {
            lock (_threadSafe)
            {
                if (!_context.CinemaFunctions.Any(x => x.MovieId == movieId && x.FunctionDateTime > DateTime.Now))
                {
                    SeedFuctions(movieId);
                }
            }
            return await _context.CinemaFunctions.Where(x => x.MovieId == movieId && x.FunctionDateTime > DateTime.Now).ToListAsync();
        }

        private void SeedFuctions(int movieId)
        {
            for (int i = 0; i < 5; i++)
            {
                DateTime randomDate = DateTime.Today.AddDays(i);

                _context.Add(new CinemaFunction()
                {
                    MovieId = movieId,
                    FunctionDateTime = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, 15, 0, 0),
                    AvailableSeats = _cinemaFunctionsSettings.SeatsNumber,
                    BasePrice = _cinemaFunctionsSettings.BasePrice
                });

                _context.Add(new CinemaFunction()
                {
                    MovieId = movieId,
                    FunctionDateTime = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, 18, 0, 0),
                    AvailableSeats = _cinemaFunctionsSettings.SeatsNumber,
                    BasePrice = _cinemaFunctionsSettings.BasePrice
                });

                _context.Add(new CinemaFunction()
                {
                    MovieId = movieId,
                    FunctionDateTime = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, 21, 0, 0),
                    AvailableSeats = _cinemaFunctionsSettings.SeatsNumber,
                    BasePrice = _cinemaFunctionsSettings.BasePrice
                });
                _context.SaveChanges();
            }
        }

        public int GetSeats(int functionId)
        {
            return _context.CinemaFunctions.First(x => x.CinemaFuctionId == functionId).AvailableSeats;

        }

        public ReserveResultDTO ProcessReserve(ReserveDTO reserveDTO)
        {
            var result = new ReserveResultDTO();
            try
            {

                lock (_threadSafe)
                {
                    var availableSeats = _context.CinemaFunctions.First(x => x.CinemaFuctionId == reserveDTO.functionId).AvailableSeats;
                    if (availableSeats < reserveDTO.NumberOfTickets)
                    {
                        result.Message = "NO SEATS";
                        result.AvailableSeats = availableSeats;
                        result.MessageCode = -1;
                        return result;
                    }

                    var paymentResult = _paymentService.Pay(reserveDTO.User, reserveDTO.NumberOfTickets);
                    if (paymentResult.Equals("OK"))
                    {
                        var cinemaReservation = new CinemaReservation();
                        cinemaReservation.CinemaFunctionId = reserveDTO.functionId;
                        cinemaReservation.CreateDate = DateTime.Now;
                        cinemaReservation.IsActive = true;
                        cinemaReservation.TotalPaid = reserveDTO.Total;
                        cinemaReservation.TotalTickets = reserveDTO.NumberOfTickets;
                        cinemaReservation.User = reserveDTO.User;
                        _context.CinemaReservations.Add(cinemaReservation);
                        var cinemaFunction = _context.CinemaFunctions.First(x => x.CinemaFuctionId == reserveDTO.functionId);
                        cinemaFunction.AvailableSeats -= reserveDTO.NumberOfTickets;
                        _context.SaveChanges();
                        result.MessageCode = 1;
                        result.Message = "OK";
                        return result;
                    }
                    result.MessageCode = -2;
                    result.Message = "PAYMENT SERVICE ERROR";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.MessageCode = -3;
                result.Message = "SYSTEM ERROR";
                return result;
            }

        }
    }
}
