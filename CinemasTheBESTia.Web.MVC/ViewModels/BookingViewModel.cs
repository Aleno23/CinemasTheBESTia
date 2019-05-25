using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Entities.Movies;

namespace CinemasTheBESTia.Web.MVC.ViewModels
{
    public class BookingViewModel
    {
        [Required]
        [Range(1, 10)]
        public Nullable<int> NumberOfTickets { get; set; }

        public double PricePerTicket { get; set; }

        public double TotalPrice { get; set; }

        public string OriginalTitle { get; set; }

        public int Id { get; set; }

        public int CinemaFuctionId { get; set; }

        public int AvailableSeats { get; set; }

    }
}
