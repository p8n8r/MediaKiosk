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
    public class MediaTests
    {
        [TestMethod()]
        public void MediaPriceTest()
        {
            Media a = new Media() { Price = "$1.23" };
            Media b = new Media() { Price = "1.23" };
            Media c = new Media() { Price = "9.87" };
            Media d = new Media() { Price = "abc" };

            Assert.AreEqual(a.Price, b.Price);
            Assert.AreNotEqual(a.Price, c.Price);
            Assert.AreNotEqual(a.Price, d.Price);
        }

        [TestMethod()]
        public void MediaTypeTest()
        {
            Media b1 = new Book();
            Media b2 = new Book();
            Media a = new Album();
            Media m = new Movie();
            Media med1 = new Media();
            Media med2 = new Media();

            Assert.AreEqual(b1.Type, b2.Type);
            Assert.AreEqual(med1.Type, med2.Type);
            Assert.AreNotEqual(b1.Type, a.Type);
            Assert.AreNotEqual(a.Type, m.Type);
        }

        [TestMethod()]
        public void InvalidMediaExceptionTest()
        {
            string property = "property";
            InvalidMediaException ex1 = new InvalidMediaException(property);
            InvalidMediaException ex2 = new InvalidMediaException(property);

            Assert.AreEqual(ex1.Property, ex2.Property);
        }
    }
}