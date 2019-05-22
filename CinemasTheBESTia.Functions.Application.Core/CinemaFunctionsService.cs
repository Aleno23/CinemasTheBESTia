using CinemasTheBESTia.Booking.Data.Context;
using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Functions.Application.Core
{
    public class CinemaFunctionsService : ICinemasFunctionsService
    {
        private readonly BookingDbContext _context;
        private static object threadSafe = new object();
        public CinemaFunctionsService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CinemaFunction>> GetFuctions(string originalTitle)
        {
            lock (threadSafe)
            {
                if (!_context.CinemaFunctions.Any(x => x.OriginalMovieTitle.Equals(originalTitle) && x.FunctionDate > DateTime.Now))
                {
                    SeedFuctions(originalTitle);
                } 
            }
            return await _context.CinemaFunctions.Where(x => x.OriginalMovieTitle.Equals(originalTitle) && x.FunctionDate > DateTime.Now).ToListAsync();
        }

        private void SeedFuctions(string originalTitle)
        {
            for (int i = 0; i < 5; i++)
            {
                DateTime randomDate = DateTime.Today.AddDays(i);

                _context.Add(new CinemaFunction()
                {
                    OriginalMovieTitle = originalTitle,
                    FunctionDate = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day),
                    FunctionTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 15, 0, 0),
                });

                _context.Add(new CinemaFunction()
                {
                    OriginalMovieTitle = originalTitle,
                    FunctionDate = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day),
                    FunctionTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 18, 0, 0),
                });

                _context.Add(new CinemaFunction()
                {
                    OriginalMovieTitle = originalTitle,
                    FunctionDate = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day),
                    FunctionTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 21, 0, 0),
                });
                _context.SaveChanges();
            }


        }
    }
}
