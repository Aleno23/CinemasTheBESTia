using CinemasTheBESTia.Entities.CinemaFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Utilities.Abstractions.Interfaces
{
    public interface IBookingService
    {
        ReserveResultDTO ProcessBooking(ReserveDTO reserveDTO);

        Task<IEnumerable<CinemaReservation>> GetBookingByUser(object user);

        Task<bool> CancelBooking(CancelDTO cancelDTO);

        Task<CinemaReservation> GetBookingById(int id);

        
    }
}
