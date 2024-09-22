using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.ViewModels.Donate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Reflection;

namespace MediaKiosk.ViewModels.Donate.Tests
{
    [TestClass()]
    public class BookDonationPageViewModelTests
    {
        [TestMethod()]
        public void BookDonationPageViewModelTest()
        {
            BookDonationPageViewModel bookDonationPageVM = new BookDonationPageViewModel();

            Assert.AreEqual(bookDonationPageVM.TitleBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(bookDonationPageVM.AuthorBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(bookDonationPageVM.CategoryBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(bookDonationPageVM.PublicationYearBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(bookDonationPageVM.CoverArtFilePathBorderBrush, Types.DEFAULT_BORDER_BRUSH);
        }

        [TestMethod()]
        public void ResetBorderTest()
        {
            BookDonationPageViewModel bookDonationPageVM = new BookDonationPageViewModel();
            PrivateObject privBookDonationPageVM = new PrivateObject(bookDonationPageVM);
            TextBox textBox = new TextBox();
            privBookDonationPageVM.Invoke("ResetBorder", textBox);
            Assert.AreEqual(textBox.BorderBrush, Types.DEFAULT_BORDER_BRUSH);
        }

        //[TestMethod()]
        //public void BrowseForImageTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()] //.\Datasets\Users.xml simulates an existing file
        [DataRow("title", "author", "category", "2000", @".\Resources\sample.png", true)]
        [DataRow("", "author", "category", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "", "category", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "author", "", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "author", "category", "xxx", @".\Resources\sample.png", false)]
        [DataRow("title", "author", "category", "2000", "BadFile.xml", false)]
        public void HasValidBookPropertiesTest(string title, string author, string category, 
            string publicationYear, string coverArtFilePath, bool shouldBeValid)
        {
            BookDonationPageViewModel bookDonationPageVM = new BookDonationPageViewModel();

            bookDonationPageVM.Title = title;
            bookDonationPageVM.Author = author;
            bookDonationPageVM.Category = category;
            bookDonationPageVM.PublicationYear = publicationYear;
            bookDonationPageVM.CoverArtFilePath = coverArtFilePath;

            bool hasValidProperties = bookDonationPageVM.HasValidBookProperties();

            Assert.AreEqual(shouldBeValid, hasValidProperties);
        }

        [TestMethod()]
        public void ClearBookPropertiesTest()
        {
            BookDonationPageViewModel bookDonationPageVM = new BookDonationPageViewModel();

            bookDonationPageVM.ClearBookProperties();

            Assert.AreEqual(bookDonationPageVM.Title, string.Empty);
            Assert.AreEqual(bookDonationPageVM.Author, string.Empty);
            Assert.AreEqual(bookDonationPageVM.Category, string.Empty);
            Assert.AreEqual(bookDonationPageVM.PublicationYear, string.Empty);
            Assert.AreEqual(bookDonationPageVM.CoverArtFilePath, string.Empty);
        }
    }
}