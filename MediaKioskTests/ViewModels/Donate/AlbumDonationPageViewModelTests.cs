﻿using MediaKiosk.DisplayDialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;

namespace MediaKiosk.ViewModels.Donate.Tests
{
    [TestClass()]
    public class AlbumDonationPageViewModelTests
    {
        [TestMethod()]
        public void AlbumDonationPageViewModelTest()
        {
            AlbumDonationPageViewModel albumDonationPageVM = new AlbumDonationPageViewModel();

            Assert.AreEqual(albumDonationPageVM.TitleBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(albumDonationPageVM.ArtistBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(albumDonationPageVM.GenreBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(albumDonationPageVM.ReleaseYearBorderBrush, Types.DEFAULT_BORDER_BRUSH);
            Assert.AreEqual(albumDonationPageVM.AlbumArtFilePathBorderBrush, Types.DEFAULT_BORDER_BRUSH);
        }

        [TestMethod()]
        public void ResetBorderTest()
        {
            AlbumDonationPageViewModel albumDonationPageVM = new AlbumDonationPageViewModel();
            PrivateObject privAlbumDonationPageVM = new PrivateObject(albumDonationPageVM);
            TextBox textBox = new TextBox();
            privAlbumDonationPageVM.Invoke("ResetBorder", textBox);
            Assert.AreEqual(textBox.BorderBrush, Types.DEFAULT_BORDER_BRUSH);
        }

        [TestMethod()]
        public void BrowseForImageTest()
        {
            AlbumDonationPageViewModel albumDonationPageVM = new AlbumDonationPageViewModel();
            PrivateObject privAlbumDonationPageVM = new PrivateObject(albumDonationPageVM);

            albumDonationPageVM.AlbumArtFilePath = null;
            albumDonationPageVM.AlbumArtFilePathBorderBrush = null;

            privAlbumDonationPageVM.SetField("displayImageFileDialog", new FakeDisplayImageFileDialog());
            privAlbumDonationPageVM.Invoke("BrowseForImage");

            Assert.IsNotNull(albumDonationPageVM.AlbumArtFilePath);
            Assert.IsNotNull(albumDonationPageVM.AlbumArtFilePathBorderBrush);
        }

        [TestMethod()] //.\Datasets\Users.xml simulates an existing file
        [DataRow("title", "artist", "genre", "2000", @".\Resources\sample.png", true)]
        [DataRow("", "artist", "genre", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "", "genre", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "artist", "", "2000", @".\Resources\sample.png", false)]
        [DataRow("title", "artist", "genre", "xxx", @".\Resources\sample.png", false)]
        [DataRow("title", "artist", "genre", "2000", "BadFile.png", false)]
        public void HasValidAlbumPropertiesTest(string title, string artist, string genre,
            string releaseYear, string albumArtFilePath, bool shouldBeValid)
        {
            AlbumDonationPageViewModel albumDonationPageVM = new AlbumDonationPageViewModel();

            albumDonationPageVM.Title = title;
            albumDonationPageVM.Artist = artist;
            albumDonationPageVM.Genre = genre;
            albumDonationPageVM.ReleaseYear = releaseYear;
            albumDonationPageVM.AlbumArtFilePath = albumArtFilePath;

            bool hasValidProperties = albumDonationPageVM.HasValidAlbumProperties();

            Assert.AreEqual(shouldBeValid, hasValidProperties);
        }

        [TestMethod()]
        public void ClearAlbumPropertiesTest()
        {
            AlbumDonationPageViewModel albumDonationPageVM = new AlbumDonationPageViewModel();

            albumDonationPageVM.ClearAlbumProperties();

            Assert.AreEqual(albumDonationPageVM.Title, string.Empty);
            Assert.AreEqual(albumDonationPageVM.Artist, string.Empty);
            Assert.AreEqual(albumDonationPageVM.Genre, string.Empty);
            Assert.AreEqual(albumDonationPageVM.ReleaseYear, string.Empty);
            Assert.AreEqual(albumDonationPageVM.AlbumArtFilePath, string.Empty);
        }
    }
}