using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models.Tests
{
    [TestClass()]
    public class MediaLibraryTests
    {
        [TestMethod()]
        public void MediaLibraryTest()
        {
            MediaLibrary lib = new MediaLibrary();

            Assert.IsNotNull(lib.Books);
            Assert.IsNotNull(lib.Albums);
            Assert.IsNotNull(lib.Movies);
        }

        [TestMethod()]
        public void SortBooksByTitleTest()
        {
            MediaLibrary lib = new MediaLibrary();
            Book b1 = new Book() { Title = "A" };
            Book b2 = new Book() { Title = "C" };
            Book b3 = new Book() { Title = "B" };
            lib.Books.Add(b1);
            lib.Books.Add(b2);
            lib.Books.Add(b3);

            lib.SortBooksByTitle();

            Assert.AreEqual(lib.Books[0].Title, "A");
            Assert.AreEqual(lib.Books[1].Title, "B");
            Assert.AreEqual(lib.Books[2].Title, "C");
        }

        [TestMethod()]
        public void SortAlbumsByTitleTest()
        {
            MediaLibrary lib = new MediaLibrary();
            Album a1 = new Album() { Title = "A" };
            Album a2 = new Album() { Title = "C" };
            Album a3 = new Album() { Title = "B" };
            lib.Albums.Add(a1);
            lib.Albums.Add(a2);
            lib.Albums.Add(a3);

            lib.SortAlbumsByTitle();

            Assert.AreEqual(lib.Albums[0].Title, "A");
            Assert.AreEqual(lib.Albums[1].Title, "B");
            Assert.AreEqual(lib.Albums[2].Title, "C");
        }

        [TestMethod()]
        public void SortMoviesByTitleTest()
        {
            MediaLibrary lib = new MediaLibrary();
            Movie m1 = new Movie() { Title = "A" };
            Movie m2 = new Movie() { Title = "C" };
            Movie m3 = new Movie() { Title = "B" };
            lib.Movies.Add(m1);
            lib.Movies.Add(m2);
            lib.Movies.Add(m3);

            lib.SortMoviesByTitle();

            Assert.AreEqual(lib.Movies[0].Title, "A");
            Assert.AreEqual(lib.Movies[1].Title, "B");
            Assert.AreEqual(lib.Movies[2].Title, "C");
        }
    }
}