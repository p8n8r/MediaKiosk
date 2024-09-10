using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    [Serializable]
    public class Album : Media
    {
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        //public decimal Length { get; set; } //Minutes
        //public string Description { get; set; }

        //public Album() { }
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
