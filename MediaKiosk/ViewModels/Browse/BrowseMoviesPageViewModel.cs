using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels.Browse
{
    internal class BrowseMoviesPageViewModel : ViewModelBase
    {
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

        public BrowseMoviesPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            //this.mainWindowViewModel = mainWindowViewModel;
            this.Movies = new ObservableCollection<Movie>(mainWindowViewModel.MediaLibrary.Movies);
        }

        //private void FillWithMovies()
        //{
        //    this.Movies = new ObservableCollection<Movie>()
        //    {
        //        new Movie() {
        //            Title = "National Treasure",
        //            Category = "Mystery",
        //            Rating = Rating.FourStars,
        //            ReleaseYear = 2007,
        //            Price = "$2.00",
        //            Stock = 1
        //        },
        //        new Movie() {
        //            Title = "Apollo 13",
        //            Category = "Docu-drama",
        //            Rating = Rating.FiveStars,
        //            ReleaseYear = 1998,
        //            Price = "$5.00",
        //            Stock = 2
        //        }
        //    };
        //}
    }
}
