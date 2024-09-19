using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    [Serializable]
    public class Movie : Media, ICloneable
    {
        public const char STAR = '\u2606';

        public string Rating { get; set; }
        public string Category { get; set; }
        public int ReleaseYear { get; set; }

        public object Clone()
        {
            return new Movie()
            {
                Title = this.Title,
                Stock = this.Stock,
                Price = this.Price,
                ArtWork = this.ArtWork,
                ArtWorkBytes = this.ArtWorkBytes,
                Rating = this.Rating,
                Category = this.Category,
                ReleaseYear = this.ReleaseYear
            };
        }
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
