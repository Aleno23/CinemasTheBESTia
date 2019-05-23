using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemasTheBESTia.Entities.CinemaFunctions
{
    public class CinemaReservation
    {

        [Key]
        public int Id { get; set; }

        public string User { get; set; }

        public double TotalPaid { get; set; }

        public int CinemaFunctionId { get; set; }

        public CinemaFunction CinemaFunction { get; set; }

        public int TotalTickets { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }
    }
}
