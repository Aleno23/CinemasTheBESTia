using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemasTheBESTia.Entities.CinemaFunctions
{
    public class CinemaFunction
    {
        [Key]
        public int CinemaFuctionId { get; set; }

        public string OriginalMovieTitle { get; set; }

        public DateTime FunctionDate { get; set; }

        public DateTime FunctionTime { get; set; }

    }
}
