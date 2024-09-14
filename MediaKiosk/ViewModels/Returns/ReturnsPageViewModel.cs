﻿using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MediaKiosk.ViewModels.Returns
{
    internal class ReturnsPageViewModel : ViewModelBase
    {
        private static readonly string[] THANKS_FOR_RETURN =
        {
            "Thanks for returning that!",
            "Thank you for bringing that back!",
            "Thanks! We hope you enjoyed it."
        };

        private MainWindowViewModel mainWindowViewModel;
        private Media selectedPurchasedMedia, selectedRentedMedia;
        private ObservableCollection<Media> purchasedMedia, rentedMedia;
        private BookComparer bookComparer = new BookComparer();
        private AlbumComparer albumComparer = new AlbumComparer();
        private MovieComparer movieComparer = new MovieComparer();
        private Random random = new Random();
        public RelayCommand returnCmd => new RelayCommand(media => Return(media), 
            media => this.SelectedRentedMedia != null);
        public RelayCommand reloadCmd => new RelayCommand(execute => ReloadMedia());

        public Media SelectedPurchasedMedia
        {
            get { return this.selectedPurchasedMedia; }
            set { this.selectedPurchasedMedia = value; OnPropertyChanged(); }
        }
        public Media SelectedRentedMedia
        {
            get { return this.selectedRentedMedia; }
            set { this.selectedRentedMedia = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Media> PurchasedMedia
        {
            get { return this.purchasedMedia; }
            set { this.purchasedMedia = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Media> RentedMedia
        {
            get { return this.rentedMedia; }
            set { this.rentedMedia = value; OnPropertyChanged(); }
        }

        public ReturnsPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel; 
        }

        private void ReloadMedia()
        {
            this.PurchasedMedia = CompileMedia(this.mainWindowViewModel.CurrentUser.Purchases);
            this.RentedMedia = CompileMedia(this.mainWindowViewModel.CurrentUser.Rentals);
        }

        private ObservableCollection<Media> CompileMedia(MediaLibrary mediaLibrary)
        {
            ObservableCollection<Media> allMedia = new ObservableCollection<Media>();
            foreach (Book book in mediaLibrary.Books)
                allMedia.Add(book);
            foreach (Album album in mediaLibrary.Albums)
                allMedia.Add(album);
            foreach (Movie movie in mediaLibrary.Movies)
                allMedia.Add(movie);
            return allMedia;
        }

        private void Return(object mediaObj)
        {
            Media media = mediaObj as Media;

            //Remove media from pending returns
            media.Stock--;
            if (media.Stock <= 0)
                this.RentedMedia.Remove(media);

            //Force refresh of media in browse page,
            //so Media subclasses can remain plain old CLR objects (pocos).
            CollectionViewSource.GetDefaultView(this.RentedMedia).Refresh();

            //Add media to library
            if (media.GetType() == typeof(Book))
            {
                Book book = media as Book;

                if (this.mainWindowViewModel.MediaLibrary.Books.Contains(book, bookComparer))
                {
                    Book bookSame = this.mainWindowViewModel.MediaLibrary.Books.Single(book, bookComparer);
                    bookSame.Stock++;
                }
                else
                {
                    book.Stock = 1;
                    this.mainWindowViewModel.MediaLibrary.Books.Add(book);
                }

                this.mainWindowViewModel.CurrentUser.Rentals.Books.Remove(book);
            }
            else if (media.GetType() == typeof(Album))
            {
                Album album = media as Album;

                if (this.mainWindowViewModel.MediaLibrary.Albums.Contains(album, albumComparer))
                {
                    Album albumSame = this.mainWindowViewModel.MediaLibrary.Albums.Single(album, albumComparer);
                    albumSame.Stock++;
                }
                else
                {
                    album.Stock = 1;
                    this.mainWindowViewModel.MediaLibrary.Albums.Add(album);
                }

                this.mainWindowViewModel.CurrentUser.Rentals.Albums.Remove(album);
            }
            else if (media.GetType() == typeof(Movie))
            {
                Movie movie = media as Movie;

                if (this.mainWindowViewModel.MediaLibrary.Movies.Contains(movie, movieComparer))
                {
                    Movie movieSame = this.mainWindowViewModel.MediaLibrary.Movies.Single(movie, movieComparer);
                    movieSame.Stock++;
                }
                else
                {
                    movie.Stock = 1;
                    this.mainWindowViewModel.MediaLibrary.Movies.Add(movie);
                }

                this.mainWindowViewModel.CurrentUser.Rentals.Movies.Remove(movie);
            }

            MessageBox.Show(THANKS_FOR_RETURN[random.Next(THANKS_FOR_RETURN.Count())]);
        }
    }
}
