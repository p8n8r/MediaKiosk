using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MediaKiosk.Models.Tests
{
    [TestClass()]
    public class MovieTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            Movie a = new Movie()
            {
                Title = "title",
                Rating = new string(Movie.STAR, 5),
                Category = "category",
                ReleaseYear = 1994,
                Price = "$0.99",
                Stock = 3,
                ArtWork = new BitmapImage(),
                ArtWorkBytes = new byte[] { 1, 2, 3, 4 }
            };

            Movie b = (Movie)a.Clone();

            Assert.AreEqual(a.Title, b.Title);
            Assert.AreEqual(a.Rating, b.Rating);
            Assert.AreEqual(a.Category, b.Category);
            Assert.AreEqual(a.ReleaseYear, b.ReleaseYear);
            Assert.AreEqual(a.Price, b.Price);
            Assert.AreEqual(a.ArtWork, b.ArtWork);
            Assert.AreEqual(a.ArtWorkBytes, b.ArtWorkBytes);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Movie a = new Movie()
            {
                Title = "title",
                Rating = new string(Movie.STAR, 5),
                Category = "category",
                ReleaseYear = 1994
            };

            Movie b = new Movie()
            {
                Title = "title",
                Rating = new string(Movie.STAR, 5),
                Category = "category",
                ReleaseYear = 1994
            };

            MovieComparer comparer = new MovieComparer();

            Assert.IsTrue(comparer.Equals(a, b));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Movie a = new Movie()
            {
                Title = "title",
                Rating = new string(Movie.STAR, 5),
                Category = "category",
                ReleaseYear = 1994
            };

            Movie b = new Movie()
            {
                Title = "title",
                Rating = new string(Movie.STAR, 5),
                Category = "category",
                ReleaseYear = 1994
            };

            MovieComparer comparer = new MovieComparer();

            Assert.AreEqual(comparer.GetHashCode(a), comparer.GetHashCode(b));
        }
    }
}