using MovieRating.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating
{
    //Varje user tilldelas en specifik movielist 
    public class User
    {
        public List<Movies> MovieList = new List<Movies>();
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public User(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }
    }
}
