using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    internal class Album : Media
    {
        public Album() { }

        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        //public decimal Length { get; set; } //Minutes
        //public string Description { get; set; }
    }
}
