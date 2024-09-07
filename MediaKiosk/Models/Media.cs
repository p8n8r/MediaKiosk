using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    enum MediaType
    {
        Books, Magazines, Albums, Movies
    }

    internal class Media : INotifyPropertyChanged
    {
        private int stock;
        private decimal price;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Stock
        {
            get { return stock; }
            set { stock = value; OnPropertyChanged(); }
        }
        public decimal Price 
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); } 
        }

        //public Media() { }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
