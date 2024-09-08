using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace MediaKiosk.Models
{
    enum MediaType
    {
        Books, Magazines, Albums, Movies
    }

    [Serializable]
    public class Media
    {
        public string Title { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public byte[] ArtWorkBytes { get; set; }
        [XmlIgnore]
        public BitmapImage ArtWork { get; set; }
    }
}
