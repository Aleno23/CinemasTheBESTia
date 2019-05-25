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

        private static object _threadSafe = new object();

        public CinemaService(BookingDbContext context, CinemaFunctionsSettings cinemaFunctionsSettings)
        {
            _context = context;
            _cinemaFunctionsSettings = cinemaFunctionsSettings;
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


        public async Task<CinemaFunction> GetCinemaFunctionById(int id)
        {
            return await _context.CinemaFunctions
                .FirstOrDefaultAsync(x => x.CinemaFuctionId == id);
        }

    }
}
