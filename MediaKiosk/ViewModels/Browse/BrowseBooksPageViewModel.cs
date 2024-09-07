using MediaKiosk.Models;
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
        private const string BOOKS_FILE_PATH = @".\Datasets\Books.csv";
        private const int TITLE_COLUMN = 0, AUTHORS_COLUMN = 1, DESCRIPTION_COLUMN = 2, CATEGORY_COLUMN = 3,
            PUBLISHER_COLUMN = 4, PUBLICATION_DATE_COLUMN = 5, PRICE_COLUMN = 6, NUM_COLUMNS_CSV = 7;
        private MainWindow mainWindow;
        private MainWindowViewModel mainWindowViewModel;
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

        public BrowseBooksPageViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.mainWindowViewModel = this.mainWindow.DataContext as MainWindowViewModel;

            //ReloadBooks(BOOKS_FILE_PATH);
            FillWithBooks();
        }

        private void FillWithBooks()
        {
            this.books = new ObservableCollection<Book>()
            {
                new Book() {
                    Title = "Holy Bible",
                    Author = "God",
                    Category = "Non-fiction",
                    PublicationYear = 0,
                    Price = 5.00M,
                    Stock = 1
                },
                new Book() {
                    Title = "Tactics",
                    Author = "Greg Cocal",
                    Category = "Self-help",
                    PublicationYear = 2000,
                    Price = 4.00M,
                    Stock = 2
                }
            };
        }

        //private void ReloadBooks(string filePath)
        //{
        //    if (File.Exists(filePath))
        //    {
        //        try
        //        {
        //            StreamReader reader = new StreamReader(filePath);
        //            string[] values;
        //            bool hasFoundHeaders = false;
        //            List<Book> books = new List<Book>();

        //            while (!reader.EndOfStream)
        //            {
        //                values = reader.ReadLine().Split(',');

        //                if (values.Length == NUM_COLUMNS_CSV)
        //                {
        //                    if (!hasFoundHeaders)
        //                    {
        //                        hasFoundHeaders = true;
        //                        continue;
        //                    }

        //                    books.Add(new Book()
        //                    {
        //                        Title = values[TITLE_COLUMN],
        //                        Author = values[AUTHORS_COLUMN],
        //                        Description = values[DESCRIPTION_COLUMN],
        //                        Category = values[CATEGORY_COLUMN],
        //                        Publisher = values[PUBLISHER_COLUMN],
        //                        PublicationDate = DateTime.Parse(values[PUBLICATION_DATE_COLUMN]),
        //                        Price = decimal.Parse(values[PRICE_COLUMN])
        //                    });
        //                }
        //            }

        //            if (books.Count > 0)
        //            {
        //                this.Books.Clear();
        //                books.ForEach(b => this.Books.Add(b)); //Copy new books over
        //            }
        //        }
        //        catch (FileNotFoundException e)
        //        {
        //            this.mainWindowViewModel.ShowErrorMessageBox(e.Message);
        //        }
        //        catch (DirectoryNotFoundException e)
        //        {
        //            this.mainWindowViewModel.ShowErrorMessageBox(e.Message);
        //        }
        //        catch (IOException e)
        //        {
        //            this.mainWindowViewModel.ShowErrorMessageBox(e.Message);
        //        }
        //    }
        //    else
        //    {
        //        string message = $"{filePath} does not exist.";
        //        this.mainWindowViewModel.ShowErrorMessageBox(message);
        //    }
        //}
    }
}
