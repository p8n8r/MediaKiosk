using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    internal class Book : Media
    {
        public Book() { }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
    }
}
