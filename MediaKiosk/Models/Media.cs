﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    enum MediaType
    {
        Books, Magazines, Albums, Movies
    }

    internal class Media
    {
        public Media() { }

        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
