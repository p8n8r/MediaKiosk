using MediaKiosk.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MediaKiosk.ViewModels.Browse
{
    public class BrowseBooksPageViewModel : ViewModelBase
    {
        private Book selectedBook;
        private ObservableCollection<Book> books;
        private MainWindowViewModel mainWindowViewModel;
        public RelayCommand reloadCmd => new RelayCommand(execute => ReloadBooks());

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
            this.mainWindowViewModel = mainWindowViewModel;
        }

        private void ReloadBooks()
        {
            this.Books = new ObservableCollection<Book>(this.mainWindowViewModel.MediaLibrary.Books);
            this.SelectedBook = this.Books.FirstOrDefault() ?? null;
        }
    }
}
