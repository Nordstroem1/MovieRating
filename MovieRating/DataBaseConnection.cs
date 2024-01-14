using MovieRating.UserControls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRating
{
    public class DataBaseConnection
    {
        //Här skall allt hämtas, filmer, users, reviews
        string server = "localhost";
        string database = "MovieRating"; 
        string username = "root";
        string password = "";
        string connectionString = "";

        List<Review> ReviewBank = new List<Review>();

        public DataBaseConnection()
        {
             connectionString =
                "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";
        }

        public List<Movies> GetAllMovies()
        {

            List<Movies> MovieBank = new List<Movies>();

            //ansluter till databasen
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM movies;";
            
            //Kör kommandot "queryn" vi skickade in, i mysql
            MySqlCommand command = new MySqlCommand(query, connection);

            //ExecuteReader() används om inget returns, vilket det inte görs i ett SELECT-kommando
            MySqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Movies movies = new Movies((int)reader["movie_id"], 
                                           (string)reader["title"],     
                                           (string)reader["genra"], 
                                           (string)reader["description"], 
                                           (string)reader["length"]);

                MovieBank.Add(movies);
            }
            connection.Close();

            return MovieBank;
        }
    }
}
