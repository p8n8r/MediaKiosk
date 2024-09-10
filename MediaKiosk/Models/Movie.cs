using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models
{
    public enum Rating { OneStar = 1, TwoStars, ThreeStars, FourStars, FiveStars }

    [Serializable]
    public class Movie : Media
    {
        public const char STAR = '\u2606';
        private Rating rating;

        public string Rating
        {
            get { return new string(STAR, (int)rating); } //Return n stars
            set
            {
                int starCnt = value.Count(c => c == STAR);
                if (starCnt >= (int)Models.Rating.OneStar &&
                    starCnt <= (int)Models.Rating.FiveStars)
                    this.rating = (Models.Rating)starCnt;
            }
        }
        public string Category { get; set; }
        public int ReleaseYear { get; set; }
        //public decimal RunTime { get; set; } //Minutes
        //public string Description { get; set; }

        //public Movie() { }
    }
}
