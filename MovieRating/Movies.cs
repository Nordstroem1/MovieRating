using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating
{
    public class Movies
    {
        public Dictionary<int, Review> ReviewDic = new Dictionary<int, Review>();  
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genra { get; set; }
        public string Length { get; set; }
        public int Id {  get; set; }


        public Movies(int id, string title, string genra, string description, string length)
        {
            Id = id;
            Title = title;
            Description = description;
            Genra = genra;
            Length = length;
        }
    }
}
