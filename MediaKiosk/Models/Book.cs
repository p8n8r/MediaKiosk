using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MediaKiosk.Models
{
    [Serializable]
    public class Book : Media, ICloneable
    {
        public string Author { get; set; }
        public string Category { get; set; }
        public int PublicationYear { get; set; }

        public object Clone()
        {
            return new Book()
            {
                Title = this.Title,
                Stock = this.Stock,
                Price = this.Price,
                ArtWork = this.ArtWork,
                ArtWorkBytes = this.ArtWorkBytes,
                Author = this.Author,
                Category = this.Category,
                PublicationYear = this.PublicationYear
            };
        }
    }

    public class BookComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            return x.Title == y.Title && x.Author == y.Author
                && x.Category == y.Category && x.PublicationYear == y.PublicationYear;
        }

        public int GetHashCode(Book book)
        {
            //Create tuple and let compiler handle the hash
            return new Tuple<string, string, string, int>
                (book.Title, book.Author, book.Category, book.PublicationYear).GetHashCode();
        }
    }
}
