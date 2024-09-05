﻿using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class BrowseMoviesPageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private Movie selectedMovie;
        private ObservableCollection<Movie> movies;

        public Movie SelectedMovie
        {
            get { return this.selectedMovie; }
            set { this.selectedMovie = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Movie> Movies
        {
            get { return this.movies; }
            set { this.movies = value; OnPropertyChanged(); }
        }

        public BrowseMoviesPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            FillWithMovies();
        }

        private void FillWithMovies()
        {
            this.Movies = new ObservableCollection<Movie>()
            {
                new Movie() {
                    Title = "wegerg",
                    Genre = "sdfgsfd",
                    RunTime = 150M,
                    Price = 2.00M,
                    Stock = 1
                },
                new Movie() {
                    Title = "agarg",
                    Genre = "arghare",
                    RunTime = 130M,
                    Price = 5.00M,
                    Stock = 2
                },
                new Movie() {
                    Title = "ojnsr",
                    Genre = "vseru",
                    RunTime = 120M,
                    Price = 6.00M,
                    Stock = 0
                },
            };
        }
    }
}