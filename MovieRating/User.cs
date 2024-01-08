using MovieRating.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating
{
    public class User
    {
        public List<Movies> MovieList = new List<Movies>();
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
