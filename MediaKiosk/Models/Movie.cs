using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    [Serializable]
    public class Movie : Media
    {
        public const char STAR = '\u2606';

        public string Rating { get; set; }
        public string Category { get; set; }
        public int ReleaseYear { get; set; }
        //public decimal RunTime { get; set; } //Minutes
        //public string Description { get; set; }

        //public Movie() { }
    }

    public class MovieComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie x, Movie y)
        {
            return x.Title == y.Title && x.Category == y.Category && x.ReleaseYear == y.ReleaseYear;
        }

        public int GetHashCode(Movie movie)
        {
            //Create tuple and let compiler handle the hash
            return new Tuple<string, string, int>
                (movie.Title, movie.Category, movie.ReleaseYear).GetHashCode();
        }
    }
}
