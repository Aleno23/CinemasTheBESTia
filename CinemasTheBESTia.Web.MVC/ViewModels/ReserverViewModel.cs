using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Web.MVC.ViewModels
{
    public class ReserverViewModel
    {
        public int AvailableSeats { get; set; }

        [Required]
        [Range(1, 10)]
        public Nullable<int> NumberOfTickets { get; set; }
        public double VoteAverage { get; set; }
        public string OriginalMovieTitle { get; set; }
        public int MovieId { get; set; }
        public int CinemaFunctionId { get; set; }
        public double PricePerTicket { get; set; }
        public double TotalPrice { get; set; }


    }
}
