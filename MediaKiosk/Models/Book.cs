using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MediaKiosk.Models
{
    internal class Book : Media
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int PublicationYear { get; set; }
        //public string Publisher { get; set; }
        //public string Description { get; set; }

        //public Book() { }
    }

    public class InvalidBookException : Exception
    {
        public InvalidBookException(string message) : base(message) { }
    }
}
