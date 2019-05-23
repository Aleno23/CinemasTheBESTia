using CinemasTheBESTia.Entities.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasTheBESTia.Web.MVC.ViewModels
{
    public class MoviesViewModel
    {
        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
    }
}
