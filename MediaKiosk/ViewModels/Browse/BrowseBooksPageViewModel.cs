﻿using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MediaKiosk.ViewModels.Browse
{
    internal class BrowseBooksPageViewModel : ViewModelBase
    {
        private Book selectedBook;
        private ObservableCollection<Book> books;

        public Book SelectedBook
        {
            get { return this.selectedBook; }
            set { this.selectedBook = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Book> Books
        {
            get { return this.books; }
            set { this.books = value; OnPropertyChanged(); }
        }

        public BrowseBooksPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.Books = new ObservableCollection<Book>(mainWindowViewModel.MediaLibrary.Books);
        }

        //private void FillWithBooks()
        //{
        //    this.books = new ObservableCollection<Book>()
        //    {
        //        new Book() {
        //            Title = "Holy Bible",
        //            Author = "God",
        //            Category = "Non-fiction",
        //            PublicationYear = 0,
        //            Price = "$5.00",
        //            Stock = 1
        //        },
        //        new Book() {
        //            Title = "Tactics",
        //            Author = "Greg Cocal",
        //            Category = "Self-help",
        //            PublicationYear = 2000,
        //            Price = "$4.00",
        //            Stock = 2
        //        }
        //    };
        //}
    }
}