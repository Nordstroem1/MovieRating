using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating
{
    class Review
    {

        public int Movie_Id { get; set; }
        public string Movie_review { get; set; }
        public DateTime Review_date { get; set; }

        public Review(string movie_review)
        {
            Movie_review = movie_review;
        }

    }
}
