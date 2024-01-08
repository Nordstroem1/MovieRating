using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating
{
    public class Movies
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genra { get; set; }
        public string Length { get; set; }
        public string Rating { get; set; }
        public int Id { get; set; } 


        public Movies(string title, string genra, string description, string length, string rating)
        {
            Id = 1; 
            Title = title;
            Description = description;
            Genra = genra;
            Length = length;
            Rating = rating;
        }
    }
}
