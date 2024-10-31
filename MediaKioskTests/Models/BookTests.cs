using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media.Imaging;

namespace MediaKiosk.Models.Tests
{
    [TestClass()]
    public class BookTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            Book a = new Book()
            {
                Title = "title",
                Author = "author",
                Category = "category",
                PublicationYear = 1994,
                Price = "$0.99",
                Stock = 3,
                ArtWork = new BitmapImage(),
                ArtWorkBytes = new byte[] { 1, 2, 3, 4 }
            };

            Book b = (Book)a.Clone();

            Assert.AreEqual(a.Title, b.Title);
            Assert.AreEqual(a.Author, b.Author);
            Assert.AreEqual(a.Category, b.Category);
            Assert.AreEqual(a.PublicationYear, b.PublicationYear);
            Assert.AreEqual(a.Price, b.Price);
            Assert.AreEqual(a.ArtWork, b.ArtWork);
            Assert.AreEqual(a.ArtWorkBytes, b.ArtWorkBytes);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Book a = new Book()
            {
                Title = "title",
                Author = "author",
                Category = "category",
                PublicationYear = 1994
            };

            Book b = new Book()
            {
                Title = "title",
                Author = "author",
                Category = "category",
                PublicationYear = 1994
            };

            BookComparer comparer = new BookComparer();

            Assert.IsTrue(comparer.Equals(a, b));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Book a = new Book()
            {
                Title = "title",
                Author = "author",
                Category = "category",
                PublicationYear = 1994
            };

            Book b = new Book()
            {
                Title = "title",
                Author = "author",
                Category = "category",
                PublicationYear = 1994
            };

            BookComparer comparer = new BookComparer();

            Assert.AreEqual(comparer.GetHashCode(a), comparer.GetHashCode(b));
        }
    }
}