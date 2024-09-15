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
    public class AlbumTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            Album a = new Album()
            {
                Title = "title",
                Artist = "artist",
                Genre = "genre",
                ReleaseYear = 1994,
                Price = "$0.99",
                Stock = 3,
                ArtWork = new BitmapImage(),
                ArtWorkBytes = new byte[] { 1, 2, 3, 4 }
            };

            Album b = (Album)a.Clone();

            Assert.AreEqual(a.Title, b.Title);
            Assert.AreEqual(a.Artist, b.Artist);
            Assert.AreEqual(a.Genre, b.Genre);
            Assert.AreEqual(a.ReleaseYear, b.ReleaseYear);
            Assert.AreEqual(a.Price, b.Price);
            Assert.AreEqual(a.ArtWork, b.ArtWork);
            Assert.AreEqual(a.ArtWorkBytes, b.ArtWorkBytes);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Album a = new Album()
            {
                Title = "title",
                Artist = "artist",
                Genre = "genre",
                ReleaseYear = 1994
            };

            Album b = new Album()
            {
                Title = "title",
                Artist = "artist",
                Genre = "genre",
                ReleaseYear = 1994
            };

            AlbumComparer comparer = new AlbumComparer();

            Assert.IsTrue(comparer.Equals(a, b));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Album a = new Album()
            {
                Title = "title",
                Artist = "artist",
                Genre = "genre",
                ReleaseYear = 1994
            };

            Album b = new Album()
            {
                Title = "title",
                Artist = "artist",
                Genre = "genre",
                ReleaseYear = 1994
            };

            AlbumComparer comparer = new AlbumComparer();

            Assert.AreEqual(comparer.GetHashCode(a), comparer.GetHashCode(b));
        }
    }
}