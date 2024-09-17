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

        public override object Clone()
        {
            Book book = (Book)base.Clone();

            book.Author = this.Author;
            book.Category = this.Category;
            book.PublicationYear = this.PublicationYear;

            return book;
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
