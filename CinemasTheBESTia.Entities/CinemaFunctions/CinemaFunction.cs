using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

    public class ReserveDTO
    {
        public int functionId { get; set; }

        public int MovieId { get; set; }

        public string User { get; set; }

        public int NumberOfTickets { get; set; }

        public double Total { get; set; }

    }

    public class ReserveResultDTO
    {
        public string Message { get; set; }

        public int MessageCode { get; set; }

        public int AvailableSeats { get; set; }

    }
}
