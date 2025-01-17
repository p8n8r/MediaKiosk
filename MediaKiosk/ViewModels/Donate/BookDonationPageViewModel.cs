﻿using MediaKiosk.DisplayDialogs;
using MediaKiosk.Models;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaKiosk.ViewModels.Donate
{
    public class BookDonationPageViewModel : ViewModelBase
    {
        public RelayCommand browseCmd => new RelayCommand(execute => BrowseForImage());
        public RelayCommand controlGotFocusCmd => new RelayCommand(sender => ResetBorder(sender));

        private string title, author, category, publicationYear, coverArtFilePath;
        private Brush titleBorderBrush, authorBorderBrush, categoryBorderBrush,
            publicationYearBorderBrush, coverArtFilePathBorderBrush;
        private readonly IDisplayImageFileDialog displayImageFileDialog;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }
        public string Author
        {
            get { return author; }
            set { author = value; OnPropertyChanged(); }
        }
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(); }
        }
        public string PublicationYear
        {
            get { return publicationYear; }
            set { publicationYear = value; OnPropertyChanged(); }
        }
        public string CoverArtFilePath
        {
            get { return coverArtFilePath; }
            set { coverArtFilePath = value; OnPropertyChanged(); }
        }
        public Brush TitleBorderBrush
        {
            get { return this.titleBorderBrush; }
            set { this.titleBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush AuthorBorderBrush
        {
            get { return this.authorBorderBrush; }
            set { this.authorBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush CategoryBorderBrush
        {
            get { return this.categoryBorderBrush; }
            set { this.categoryBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush PublicationYearBorderBrush
        {
            get { return this.publicationYearBorderBrush; }
            set { this.publicationYearBorderBrush = value; OnPropertyChanged(); }
        }
        public Brush CoverArtFilePathBorderBrush
        {
            get { return this.coverArtFilePathBorderBrush; }
            set { this.coverArtFilePathBorderBrush = value; OnPropertyChanged(); }
        }

        public BookDonationPageViewModel()
        {
            this.displayImageFileDialog = new DisplayImageFileDialog();
            InitializeBorders();
        }

        private void InitializeBorders()
        {
            //Initialize all border colors
            this.TitleBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.AuthorBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.CategoryBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.PublicationYearBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            this.CoverArtFilePathBorderBrush = Types.DEFAULT_BORDER_BRUSH;
        }

        private void ResetBorder(object sender)
        {
            (sender as Control).BorderBrush = Types.DEFAULT_BORDER_BRUSH;
        }

        private void BrowseForImage()
        {
            //Select image in dialog
            bool? result = this.displayImageFileDialog.OpenImageBrowser();

            if (result == true)
            {
                //Save image file path
                this.CoverArtFilePath = this.displayImageFileDialog.FilePath;

                //Reset border color
                this.CoverArtFilePathBorderBrush = Types.DEFAULT_BORDER_BRUSH;
            }
        }

        public bool HasValidBookProperties()
        {
            bool hasValidProperties = true;

            if (string.IsNullOrWhiteSpace(this.Title))
            {
                this.TitleBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.Author))
            {
                this.AuthorBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.Category))
            {
                this.CategoryBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (!int.TryParse(this.PublicationYear, out int pubYear)
                || (pubYear < 0 || pubYear > DateTime.Now.Year))
            {
                this.PublicationYearBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            if (string.IsNullOrWhiteSpace(this.CoverArtFilePath)
                || !File.Exists(this.CoverArtFilePath))
            {
                this.CoverArtFilePathBorderBrush = Types.INVALID_BORDER_BRUSH;
                hasValidProperties = false;
            }

            return hasValidProperties;
        }

        public void ClearBookProperties()
        {
            this.Title = string.Empty;
            this.Author = string.Empty;
            this.Category = string.Empty;
            this.PublicationYear = string.Empty;
            this.CoverArtFilePath = string.Empty;
        }
    }
}
