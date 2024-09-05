using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    internal class Movie
    {
        public Movie() { }

        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal RunTime { get; set; } //Minutes
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
    }
}
