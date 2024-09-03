using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels
{
    internal class BrowseBooksPageViewModel : ViewModelBase
    {
        private MainWindow mainWindow;
        private Book selectedBook;
        private ObservableCollection<Book> books;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set { this.selectedBook = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Book> Books
        {
            get { return books; }
            set { this.books = value; OnPropertyChanged(); }
        }

        public BrowseBooksPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            FillWithBooks();
        }

        private void FillWithBooks()
        {
            this.books = new ObservableCollection<Book>()
            {
                new Book() {
                    Title = "xxxxxxx",
                    Author = "yyyyy",
                    Category = "xxxxxx",
                    Price = 3.00M,
                    Stock = 1
                },
                new Book() {
                    Title = "qqqq",
                    Author = "rrrr",
                    Category = "yyyy",
                    Price = 4.00M,
                    Stock = 2
                },
                new Book() {
                    Title = "ytty",
                    Author = "arfar",
                    Category = "afgag",
                    Price = 1.00M,
                    Stock = 0
                }
            };
        }
    }
}
