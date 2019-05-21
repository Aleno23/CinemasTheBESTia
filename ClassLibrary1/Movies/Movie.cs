using System;

namespace CinemasTheBESTia.Entities.Movies
{
    public class Movie
    {

        public double VoteAverage { get; set; }

        public string OriginalTitle { get; set; }

        public string OriginalLanguage { get; set; }

        public string Overview { get; set; }

        public DateTime ReleaseDate { get; set; }
      
        public string PosterPath { get; set; }

        public string FullPosterPath { get; set; }

        

    }
}
