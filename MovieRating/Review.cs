using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating
{
    public class Review
    {

        public int Review_Id { get; set; }  
        public string Movie_review { get; set; }
        public DateTime Review_date { get; set; }

        public Review(int id, string movie_review)
        {
            Review_Id = id;
            Movie_review = movie_review;
            Review_date = DateTime.Now;
        }
    }
}
