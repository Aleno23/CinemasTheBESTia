using CinemasTheBESTia.Entities.CinemaFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Utilities.Abstractions.Interfaces
{
    public interface ICinemasFunctionsService
    {
        Task<IEnumerable<CinemaFunction>> GetFuctions(string originalTitle);
    }
}
