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
        public decimal Length { get; set; } //Minutes
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
    }
}
