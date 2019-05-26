using System;
using System.ComponentModel.DataAnnotations;

namespace CinemasTheBESTia.Entities.CinemaFunctions
{
    public class CinemaFunction
    {
        [Key]
        public int CinemaFuctionId { get; set; }

        public int MovieId { get; set; }

        public DateTime FunctionDateTime { get; set; }

        public double BasePrice { get; set; }

        public int AvailableSeats { get; set; }

    }

  
}
