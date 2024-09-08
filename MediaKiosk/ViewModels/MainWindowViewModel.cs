using MediaKiosk.Models;
using MediaKiosk.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaKiosk.ViewModels
{
    internal class MainWindowViewModel
    {
        private MainWindow mainWindow;
        private const string MEDIA_LIBRARY_FILE = @".\Datasets\MediaLibrary.lib";
        internal MediaLibrary MediaLibrary { get; set; }
        //private const string BOOKS_FILE = @".\Datasets\Books.csv",
        //    ALBUMS_FILE = @".\Datasets\Albums.csv", MOVIES_FILE = @".\Datasets\Movies.csv";
        //private const int BOOK_TITLE_COL = 0, BOOK_AUTHORS_COL = 1, BOOK_DESCRIPTION_COL = 2, 
        //    BOOK_CATEGORY_COL = 3, BOOK_PUBLICATION_DATE_COL = 4, BOOK_PRICE_COL = 5,
        //    BOOK_StTOCK_COL = 5, BOOK_NUM_COLUMNS_CSV = 6;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            //ImportMediaLibrary();
        }

        private void ExportMediaLibrary()
        {
            FileStream stream = new FileStream(MEDIA_LIBRARY_FILE, FileMode.Create, FileAccess.Write);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (stream)
                {
                    formatter.Serialize(stream, this.MediaLibrary);
                }
            }
            catch (ArgumentNullException e)
            {
                Utility.ShowErrorMessageBox(e.Message);
            }
            catch (SerializationException e)
            {
                Utility.ShowErrorMessageBox(e.Message);
            }
            catch (SecurityException e)
            {
                Utility.ShowErrorMessageBox(e.Message);
            }
        }

        private void ImportMediaLibrary()
        {
            FileStream stream = new FileStream(MEDIA_LIBRARY_FILE, FileMode.Open, FileAccess.Read, FileShare.None);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (stream)
                {
                    this.MediaLibrary = (MediaLibrary)formatter.Deserialize(stream);
                }
            }
            catch (ArgumentNullException e)
            {
                Utility.ShowErrorMessageBox(e.Message);
            }
            catch (SerializationException e)
            {
                Utility.ShowErrorMessageBox(e.Message);
            }
            catch (SecurityException e)
            {
                Utility.ShowErrorMessageBox(e.Message);
            }
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

        //                if (values.Length == BOOK_NUM_COLUMNS_CSV)
        //                {
        //                    if (!hasFoundHeaders)
        //                    {
        //                        hasFoundHeaders = true;
        //                        continue;
        //                    }

        //                    books.Add(new Book()
        //                    {
        //                        Title = values[BOOK_TITLE_COL],
        //                        Author = values[BOOK_AUTHORS_COL],
        //                        Category = values[BOOK_CATEGORY_COL],
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
