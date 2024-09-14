using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    [Serializable]
    public class Album : Media, ICloneable
    {
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }

        public object Clone()
        {
            return new Album()
            {
                Title = this.Title,
                Artist = this.Artist,
                Genre = this.Genre,
                ReleaseYear = this.ReleaseYear,
                Stock = this.Stock,
                Price = this.Price,
                ArtWork = this.ArtWork,
                ArtWorkBytes = this.ArtWorkBytes
            };
        }
    }

    public class AlbumComparer : IEqualityComparer<Album>
    {
        public bool Equals(Album x, Album y)
        {
            return x.Title == y.Title && x.Artist == y.Artist
                && x.Genre == y.Genre && x.ReleaseYear == y.ReleaseYear;
        }

        public int GetHashCode(Album album)
        {
            //Create tuple and let compiler handle the hash
            return new Tuple<string, string, string, int>
                (album.Title, album.Artist, album.Genre, album.ReleaseYear).GetHashCode();
        }
    }
}
