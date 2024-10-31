using System;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace MediaKiosk.Models
{
    public enum MediaType
    {
        Books, Albums, Movies
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
                string priceStr = value.Replace("$", ""); //Remove '$'
                decimal.TryParse(priceStr, out price);
            }
        }
        public byte[] ArtWorkBytes { get; set; }
        [XmlIgnore]
        public BitmapImage ArtWork { get; set; }
        public string Type { get { return this.GetType().Name; } } //Needed for data binding
    }
}
