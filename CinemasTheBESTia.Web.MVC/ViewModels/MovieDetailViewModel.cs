using CinemasTheBESTia.Entities.CinemaFunctions;
using CinemasTheBESTia.Entities.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Web.MVC.ViewModels
{
    public class MovieDetailViewModel
    {
        public Movie Movie { get; set; }

        public IEnumerable<CinemaFunction> CinemaFunctions { get; set; }

        [Required(ErrorMessage = "Error: Must Choose a Function")]
        public int CinemaFunctionID { get; set; }

        public int AvailableSeats { get; set; }

        
        public int NumberOfTickets { get; set; }


    }
}
