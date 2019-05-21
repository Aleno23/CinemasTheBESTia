using CinemasTheBESTia.Entities.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Utilities.Abstractions.Interfaces
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetMovies();
    }
}
