using CinemasTheBESTia.Entities.CinemaFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Utilities.Abstractions.Interfaces
{
    public interface ICinemasService
    {
        Task<IEnumerable<CinemaFunction>> GetFuctions(int movieId);
        int GetSeats(int functionId);
        ReserveResultDTO ProcessReserve(ReserveDTO reserveDTO);
    }
}
