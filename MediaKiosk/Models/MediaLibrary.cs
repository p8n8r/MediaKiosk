using MediaKiosk.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    [Serializable]
    public class MediaLibrary
    {
        public List<Book> Books { get; set; }
        public List<Album> Albums { get; set; }
        public List<Movie> Movies { get; set; }

        public MediaLibrary()
        {
            this.Books = new List<Book>();
            this.Albums = new List<Album>();
            this.Movies = new List<Movie>();
        }

        public void SortBooksByTitle()
        {
            this.Books = this.Books.OrderBy(b => b.Title).ToList();
        }

        public void SortAlbumsByTitle()
        {
            this.Albums = this.Albums.OrderBy(b => b.Title).ToList();
        }

        public void SortMoviesByTitle()
        {
            this.Movies = this.Movies.OrderBy(b => b.Title).ToList();
        }
    }
}
