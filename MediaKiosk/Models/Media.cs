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
        private decimal price;
        public string Title { get; set; }
        public int Stock { get; set; }
        public string Price
        {
            get { return price.ToString("C"); }
            set
            {
                string priceStr = value.Replace("$", "");
                decimal.TryParse(priceStr, out price);
            }
        }
        public byte[] ArtWorkBytes { get; set; }
        [XmlIgnore]
        public BitmapImage ArtWork { get; set; }
    }

    public class InvalidMediaException : Exception
    {
        public string Property { get; set; }
        public InvalidMediaException(string message, string property=null) : base(message)
        { 
            this.Property = property;
        }
    }
}
