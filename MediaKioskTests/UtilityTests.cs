using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Windows.Media.Imaging;

namespace MediaKiosk.Tests
{
    [TestClass()]
    public class UtilityTests
    {
        [TestMethod()]
        public void BitmapImageConversionTest()
        {
            BitmapImage image = new BitmapImage(new Uri(@".\Resources\sample.png", UriKind.Relative));
            byte[] bytes = Utility.ConvertBitmapImageToBytes(image);
            BitmapImage imageReturned = Utility.ConvertBytesToBitmapImage(bytes);
            byte[] bytesReturned = Utility.ConvertBitmapImageToBytes(image);

            Assert.AreEqual(image.Width, imageReturned.Width, 0.1d);
            Assert.AreEqual(image.Height, imageReturned.Height, 0.1d);
            Assert.AreEqual(image.DpiX, imageReturned.DpiX, 0.1d);
            Assert.AreEqual(image.DpiY, imageReturned.DpiY, 0.1d);
            Assert.AreEqual(image.Format, imageReturned.Format);

            Assert.IsTrue(bytes.SequenceEqual(bytesReturned));
        }

        [TestMethod()]
        [DataRow(1d, 10d)]
        [DataRow(10d, 100d)]
        public void GetRandomDollarValueTest(double lowDobule, double highDouble)
        {
            decimal low = Convert.ToDecimal(lowDobule);
            decimal high = Convert.ToDecimal(highDouble);

            string dollarsString = Utility.GetRandomDollarValue(low, high);
            Assert.IsNotNull(dollarsString);

            decimal dollars = Convert.ToDecimal(dollarsString.Replace("$", ""));
            Assert.IsTrue(dollars >= low && dollars < high);
        }
    }
}