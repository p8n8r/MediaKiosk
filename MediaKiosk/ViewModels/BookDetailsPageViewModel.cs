using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class BookDetailsPageViewModel : ViewModelBase
    {
        private MainWindow mainWindow; 
        private Book selectedBook;
        public Book SelectedBook
        {
            get { return this.selectedBook; }
            set { this.selectedBook = value; OnPropertyChanged(); }
        }

        public BookDetailsPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.SelectedBook = new Book();
        }

        internal void SetDetailsForDonations()
        {
            this.SelectedBook.Stock = 0;
            this.SelectedBook.Price = 0.00M;
            //TODO: disable textbox
        }
    }
}
