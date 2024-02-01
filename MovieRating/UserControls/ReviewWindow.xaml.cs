using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieRating.UserControls
{
    public partial class ReviewWindow : UserControl
    {
        MovieMenu movieMenu;

        string server = "localhost";
        string database = "MovieRating";
        string username = "root";
        string password = "";
        string connectionString = "";

        public ReviewWindow()
        {
            InitializeComponent();
        }
        //Sätter värdet på movieMenu
        public void SetMovieMenu(MovieMenu movieMenu)
        {
            this.movieMenu = movieMenu;
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        //Lägger till Review i databasen, DateTime används inte i programmet men loggas enbart i databasen.
        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            string review = null;
            Movies movie = movieMenu.GetChosenMovie();
            int movie_id = movie.Id;
            DateTime date = DateTime.Now;

            if (Create_review_box.Text != "")
            {
                review = Create_review_box.Text + "\n";
                Review newReview = new Review(movie_id, review);
                Create_review_box.Text = "";

                MySqlConnection connection = new MySqlConnection(connectionString =
                                                           "SERVER=" + server + ";" +
                                                           "DATABASE=" + database + ";" +
                                                           "UID=" + username + ";" +
                                                           "PASSWORD=" + password + ";");

                connection.Open();

                string query = "INSERT INTO review(movie_id, review_date, user_review) VALUES(@movie_id, @review_date, @user_review);";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_review", review);
                command.Parameters.AddWithValue("@review_date", date);
                command.Parameters.AddWithValue("@movie_id", movie_id);

                command.ExecuteNonQuery();
                Create_review_box.Clear();
                connection.Close();
            }
            this.Visibility = Visibility.Hidden;
        }

        //hämtar alla reviews som tillhör rätt film från Databasen, retunerar en listan till review window!
        public List<Review> GetReviews()
        {
            Movies chosenMovie = movieMenu.GetChosenMovie();
            if (movieMenu.UserMovies_box.Items.Contains(chosenMovie))
            {



                List<Review> reviewList = new List<Review>();
                int movie_id = chosenMovie.Id;
                int review_id = 0;

                MySqlConnection connection = new MySqlConnection(connectionString =
                                                               "SERVER=" + server + ";" +
                                                               "DATABASE=" + database + ";" +
                                                               "UID=" + username + ";" +
                                                               "PASSWORD=" + password + ";");
                connection.Open();

                string query = "SELECT * FROM review WHERE movie_id = @movie_id;";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@movie_id", movie_id);
                command.Parameters.AddWithValue("@review_id", review_id);
                MySqlDataReader reader = command.ExecuteReader();

                //Lägger till reviewn om den inte redan existerar
                while (reader.Read())
                {
                    Review review = new Review((int)reader["review_id"], (string)reader["user_review"]);

                    if (!chosenMovie.ReviewDic.ContainsKey(review.Review_Id))
                    {
                        chosenMovie.ReviewDic.Add(review.Review_Id, review);
                    }
                }

                foreach (Review review in chosenMovie.ReviewDic.Values)
                {
                    reviewList.Add(review);
                }
                connection.Close();
                return reviewList;
            }
            return null;
        }
    }
} 
