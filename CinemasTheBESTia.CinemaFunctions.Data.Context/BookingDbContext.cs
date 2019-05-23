using CinemasTheBESTia.Entities.CinemaFunctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace CinemasTheBESTia.Booking.Data.Context
{
    public class BookingDbContext : DbContext
    {

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {

        }

        public DbSet<CinemaFunction> CinemaFunctions { get; set; }
        public DbSet<CinemaReservation> CinemaReservations { get; set; }
    }


}
