﻿using MovieRating.UserControls;
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
        string server = "localhost";
        string database = "MovieRating"; 
        string username = "root";
        string password = "";
        string connectionString = "";

        Dictionary<int, User> userDic = new Dictionary<int, User>();

        public DataBaseConnection()
        {
             connectionString =
                "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";
        }

        //hämtar användare, filmer och get filmer till rätt användare.
        public void GiveMoviesToUser(User user1)
        {
            List<Movies> UserMovies = new List<Movies>();
            Dictionary<int,Movies> UserMovieDic = new Dictionary<int,Movies>();

            //ansluter till databasen
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM user_movies;";
        
            MySqlCommand command = new MySqlCommand(query, connection);

            //ExecuteReader() används om n rågot retuneras, vilket det görs i ett SELECT-kommando
            MySqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int movie_id = (int)reader["movie_id"];
                int user_id = (int)reader["user_id"];

                //om movien inte finns i dictionaryn, skapa den
                if (!UserMovieDic.ContainsKey((int)reader["movie_id"])) 
                {
                    Movies movies = new Movies((int)reader["movie_id"],
                                          (string)reader["title"],
                                          (string)reader["genra"],
                                          (string)reader["description"],
                                          (string)reader["length"]);

                    UserMovieDic.Add((int)reader["movie_id"], movies);
                }

                //om usern inte finns i dictionaryn, skapa den
                if (!userDic.ContainsKey(user_id))
                {
                    User user = new User((int)reader["user_id"],
                                        (string)reader["username"], 
                                        (string)reader["PASSWORD"]);

                    userDic.Add((int)reader["user_id"], user);
                }

                if(user1.Id == user_id)
                {
                    //Ger rätt film till den inloggade användaren
                    user1.MovieList.Add(UserMovieDic[movie_id]);
                }
            }
             connection.Close();
        }
        
        //denna funktion ska ge värdet till MovieListboxen där samtliga filmer dyker upp
        public List<Movies> GetAllMovies()
        {
            List<Movies> MovieBank = new List<Movies>();
            Dictionary<int, Movies> MovieDic = new Dictionary<int, Movies>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM movies;";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader(); 

            while (reader.Read())
            {
                if (!MovieDic.ContainsKey((int)reader["movie_id"]))
                {
                    Movies movie = new Movies((int)reader["movie_id"],
                                          (string)reader["title"],
                                          (string)reader["genra"],
                                          (string)reader["description"],
                                          (string)reader["length"]);

                    MovieDic.Add((int)reader["movie_id"], movie);   
                }
            }

            foreach (Movies movie in MovieDic.Values)
            {
                MovieBank.Add(movie);
            }
            connection.Close();
            return MovieBank;
        }
    }
}