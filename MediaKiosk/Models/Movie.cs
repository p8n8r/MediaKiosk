using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    enum Rating { OneStar = 1, TwoStars, ThreeStars, FourStars, FiveStars }

    internal class Movie : Media
    {
        public Movie() { }

        public string Title { get; set; }
        public string Category { get; set; }
        public Rating Rating { get; set; }
        public int ReleaseYear { get; set; }
        //public decimal RunTime { get; set; } //Minutes
        //public string Description { get; set; }
    }
}
