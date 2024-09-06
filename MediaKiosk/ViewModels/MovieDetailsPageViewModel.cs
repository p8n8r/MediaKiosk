﻿using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class MovieDetailsPageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private Movie selectedMovie;
        public Movie SelectedMovie
        {
            get { return this.selectedMovie; }
            set { this.selectedMovie = value; OnPropertyChanged(); }
        }

        public MovieDetailsPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.SelectedMovie = new Movie();
        }

        internal void SetDetailsForDonations()
        {
            this.SelectedMovie.Stock = 0;
            this.SelectedMovie.Price = 0.00M;
            //TODO: disable textbox
        }
    }
}
