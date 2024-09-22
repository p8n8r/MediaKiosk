using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels.Donate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels.Donate.Tests
{
    [TestClass()]
    public class MovieDonationPageViewModelTests
    {
        [TestMethod()]
        public void MovieDonationPageViewModelTest()
        {
            MovieDonationPageViewModel movieDonationPageVM = new MovieDonationPageViewModel();

            Assert.AreEqual(movieDonationPageVM.TitleBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(movieDonationPageVM.RatingBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(movieDonationPageVM.CategoryBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(movieDonationPageVM.ReleaseYearBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(movieDonationPageVM.PromoArtFilePathBorderBrush, Types.DEFAULT_BORDER_BRUSH);
        }

        [TestMethod()]
        public void ResetBorderTest()
        {
            MovieDonationPageViewModel movieDonationPageVM = new MovieDonationPageViewModel();
            PrivateObject privMovieDonationPageVM = new PrivateObject(movieDonationPageVM);
            TextBox textBox = new TextBox();
            privMovieDonationPageVM.Invoke("ResetBorder", textBox);
            Assert.AreEqual(textBox.BorderBrush, Types.DEFAULT_BORDER_BRUSH);
        }

        //[TestMethod()]
        //public void BrowseForImageTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()] //.\Datasets\Users.xml simulates an existing file
        [DataRow("title", "rating", "genre", "2000", @".\Resources\sample.png", true)]
        [DataRow("", "rating", "genre", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "", "genre", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "rating", "", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "rating", "genre", "xxx", @".\Resources\sample.png", false)]
        [DataRow("title", "rating", "genre", "2000", "BadFile.png", false)]
        public void HasValidMoviePropertiesTest(string title, string rating, string genre,
            string releaseYear, string promoArtFilePath, bool shouldBeValid)
        {
            MovieDonationPageViewModel movieDonationPageVM = new MovieDonationPageViewModel();

            movieDonationPageVM.Title = title;
            movieDonationPageVM.Rating = rating;
            movieDonationPageVM.Category = genre;
            movieDonationPageVM.ReleaseYear = releaseYear;
            movieDonationPageVM.PromoArtFilePath = promoArtFilePath;

            bool hasValidProperties = movieDonationPageVM.HasValidMovieProperties();

            Assert.AreEqual(shouldBeValid, hasValidProperties);
        }

        [TestMethod()]
        public void ClearMoviePropertiesTest()
        {
            MovieDonationPageViewModel movieDonationPageVM = new MovieDonationPageViewModel();

            movieDonationPageVM.ClearMovieProperties();

            Assert.AreEqual(movieDonationPageVM.Title, string.Empty);
            Assert.AreEqual(movieDonationPageVM.Rating, string.Empty);
            Assert.AreEqual(movieDonationPageVM.Category, string.Empty);
            Assert.AreEqual(movieDonationPageVM.ReleaseYear, string.Empty);
            Assert.AreEqual(movieDonationPageVM.PromoArtFilePath, string.Empty);
        }
    }
}