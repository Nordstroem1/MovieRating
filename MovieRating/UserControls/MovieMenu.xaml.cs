using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MovieRating.UserControls
{
    /// <summary>
    /// Interaction logic for MovieMenu.xaml
    /// </summary>
    public partial class MovieMenu : UserControl
    {
        Login login;
        User currentuser;
        Movies chosenMovie;
        public List<Movies> MovieBankCopy = new List<Movies>();
        MovieManager movieManager;
        ReviewWindow ReviewWindow;
       
        string server = "localhost";
        string database = "MovieRating";
        string username = "root";
        string password = "";
        string connectionString = "";


        public MovieMenu()
        {
            InitializeComponent();
        }

        //sättervärdet av login
        public void SetWindows(Login login, MovieManager movieManager, ReviewWindow reviewWindow)
        {
            this.login = login;
            this.movieManager = movieManager;
            this.ReviewWindow = reviewWindow;
        }
        private void AddMovie_btn_Click(object sender, RoutedEventArgs e)
        {
            if (MovieBank_box.Items.Count >= 1)
            {
                AddMovies();
            }
        }
        
        private void RemoveMovie_btn_Click(object sender, RoutedEventArgs e)
        {
            RemoveMovie();
        }

        //Lägger till filmer som är befintliga i databasen, dem läggs till i användarens lista.
        //Om inte filmen redan finns i listan.
        private void AddMovies()
        {
            currentuser = login.GetCurrentUserLogin();
            var EditItem = MovieBank_box.SelectedItem;

            if (!currentuser.MovieList.Contains(EditItem))
            {
                currentuser.MovieList.Add((Movies)EditItem);
                AddMoviesToDb();
                UserMovies_box.ItemsSource = currentuser.MovieList;
                UserMovies_box.Items.Refresh();
            }
            else if(UserMovies_box.SelectedItems.Contains(EditItem))
            {
                Error_label.Visibility = Visibility.Visible;
            }
        }

        //Lägg till kopplingen där man anger vilken film som tillhör vem.
        private void AddMoviesToDb()
        {
            currentuser = login.GetCurrentUserLogin();
            var EditItem = MovieBank_box.SelectedItem;

            //ansluter till databasen
            MySqlConnection connection = new MySqlConnection(connectionString =
                                                            "SERVER=" + server + ";" +
                                                            "DATABASE=" + database + ";" +
                                                            "UID=" + username + ";" +
                                                            "PASSWORD=" + password + ";");

            connection.Open();
            int user_id = currentuser.Id;
            int movie_id = ((Movies)EditItem).Id;

            string query = "INSERT INTO user_movies_lt(user_id, movie_id) VALUES(@user_id, @movie_id);";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@movie_id", movie_id);
            command.Parameters.AddWithValue("@user_id", user_id);

            command.ExecuteNonQuery();
            connection.Close();
        }

        //Tar bort valda indexet i listboxen av filmer använadren samlat ihop
        private void RemoveMovie()
        {
            currentuser = login.GetCurrentUserLogin();
            var EditItem = UserMovies_box.SelectedItem;
            var ChoosenMovie = MovieBank_box.SelectedItem;

            if (UserMovies_box.Items.Count > -1)
            {
                currentuser.MovieList.Remove((Movies)EditItem);
                UserMovies_box.Items.Refresh();

                MySqlConnection connection = new MySqlConnection(connectionString =
                                                            "SERVER=" + server + ";" +
                                                            "DATABASE=" + database + ";" +
                                                            "UID=" + username + ";" +
                                                            "PASSWORD=" + password + ";");

                connection.Open();
                int user_id = currentuser.Id;
                int movie_id = ((Movies)EditItem).Id;

                string query = "DELETE FROM user_movies_lt WHERE user_id = @user_id AND movie_id = @movie_id;";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@movie_id", movie_id);
                command.Parameters.AddWithValue("@user_id", user_id);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Läser in datan från databasen
        public void LoadMoviesFromDB()
        {
            currentuser = login.GetCurrentUserLogin();
            DataBaseConnection db = new DataBaseConnection();
            db.GiveMoviesToUser(currentuser);
            UserMovies_box.ItemsSource = currentuser.MovieList;
            MovieBank_box.ItemsSource = db.GetAllMovies();
            MovieBankCopy = db.GetAllMovies();
        }


        private void Drama_movies_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, Movies> DramaDic = new Dictionary<int, Movies>();

            MySqlConnection connection = new MySqlConnection(connectionString =
                                                            "SERVER=" + server + ";" +
                                                            "DATABASE=" + database + ";" +
                                                            "UID=" + username + ";" +
                                                            "PASSWORD=" + password + ";");

            connection.Open();

            string query = "SELECT * FROM movies WHERE genra LIKE '%Drama%';";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Movies movie = new Movies((int)reader["movie_id"],
                                          (string)reader["title"],
                                          (string)reader["genra"],
                                          (string)reader["description"],
                                          (string)reader["length"]);

                DramaDic.Add((int)reader["movie_id"], movie);
            }

            MovieBank_box.ItemsSource = DramaDic.Values;
            connection.Close();
        }

        private void LengtBtn_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, Movies> LengthDic = new Dictionary<int, Movies>();

            MySqlConnection connection = new MySqlConnection(connectionString =
                                                            "SERVER=" + server + ";" +
                                                            "DATABASE=" + database + ";" +
                                                            "UID=" + username + ";" +
                                                            "PASSWORD=" + password + ";");

            connection.Open();

            string query = "SELECT * FROM movies ORDER BY length ASC";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Movies movie = new Movies((int)reader["movie_id"],
                                          (string)reader["title"],
                                          (string)reader["genra"],
                                          (string)reader["description"],
                                          (string)reader["length"]);

                LengthDic.Add((int)reader["movie_id"], movie);
            }

            MovieBank_box.ItemsSource = LengthDic.Values;
            connection.Close();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            DataBaseConnection db = new DataBaseConnection();
            MovieBank_box.ItemsSource = MovieBankCopy;
        }

        //Tar användaren till MovieManager där man kan skapa en obefintlig film
        private void CreateMovie_btn_Click(object sender, RoutedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                movieManager.Movie_box.ItemsSource = currentuser.MovieList;
                movieManager.Visibility = Visibility.Visible;
                this.Visibility = Visibility.Hidden;
            }
        }



        private void Review_btn_Click(object sender, RoutedEventArgs e)
        {
            chosenMovie = GetChosenMovie();

            if (UserMovies_box.SelectedItem == chosenMovie)
            {
                ReviewWindow.Add_Btn.Visibility = Visibility.Visible;
                ReviewWindow.Create_review_box.Visibility = Visibility.Visible;
                ReviewWindow.Review_box.Visibility = Visibility.Hidden;
                ReviewWindow.Visibility = Visibility.Visible;
            }
        }

        private void Read_review_btn_Click(object sender, RoutedEventArgs e)
        {
            var chosenMovie = UserMovies_box.SelectedItem;

            if (UserMovies_box.Items.Count > -1)
            {

                foreach (Review review in ReviewWindow.GetReviews())
                {
                    ReviewWindow.Review_box.Items.Add(review.Movie_review);
                }

                ReviewWindow.Visibility = Visibility.Visible;
                ReviewWindow.Review_box.Visibility = Visibility.Visible;
                ReviewWindow.Add_Btn.Visibility = Visibility.Hidden;
                ReviewWindow.Create_review_box.Visibility = Visibility.Hidden;
                ReviewWindow.Create_review_box.Visibility = Visibility.Hidden;
            }
            ReviewWindow.Review_box.Items.Refresh();
        }

        //Ger tillgång till den aktuella filmen till ReviewWindow och MovieMenu
        public Movies GetChosenMovie()
        {
            chosenMovie = (Movies)UserMovies_box.SelectedItem;
            return chosenMovie;
        }
    }
}
