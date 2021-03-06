﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemasTheBESTia.Entities.CinemaFunctions
{
    public class CinemaReservation
    {

        [Key]
        public int Id { get; set; }

        [DisplayName("User")]
        public string User { get; set; }

        [DisplayName("Total Paid")]
        public double TotalPaid { get; set; }

        public int CinemaFunctionId { get; set; }

        public CinemaFunction CinemaFunction { get; set; }

        [DisplayName("Total Tickets")]
        public int TotalTickets { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        [DisplayName("Movie Title")]
        public string OriginalMovieTitle { get; set; }
        
    }
}
