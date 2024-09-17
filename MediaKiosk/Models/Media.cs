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
    public enum MediaType
    {
        Books, Albums, Movies
    }

    [Serializable]
    public class Media : ICloneable
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

        public virtual object Clone()
        {
            return new Book()
            {
                Title = this.Title,
                Stock = this.Stock,
                Price = this.Price,
                ArtWork = this.ArtWork,
                ArtWorkBytes = this.ArtWorkBytes
            };
        }
    }
}
