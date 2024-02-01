using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit;
using ZstdSharp.Unsafe;

namespace MovieRating.UserControls
{
    public partial class MovieManager : UserControl
    {
        
        Login login;
        MovieMenu movieMenu;
        User currentuser = null;
        string server = "localhost";
        string database = "MovieRating";
        string username = "root";
        string password = "";
        string connectionString = "";

        public MovieManager()
        {
            InitializeComponent();
        }

        
        //Sätter värdet av Moviemenu och login
        public void SetWindows(MovieMenu movieMenu, Login login)
        {
            this.movieMenu = movieMenu;
            this.login = login;
        }
        
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            AddMovies();   
        }
         
        //Tar bort filmen man valt i listboxen. om den finns. 
        private void Remove_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Movie_box.SelectedItems.Count > 0)
            {
                while(Movie_box.SelectedIndex > -1)
                {
                    currentuser.MovieList.RemoveAt(Movie_box.SelectedIndex);
                    Movie_box.Items.Refresh();
                }
            }
        }

        //Användaren lägger till sin skapade film i databasen.
        private void AddMovies()
        {
            currentuser = login.GetCurrentUserLogin();
            int id = 0;
            string title = null;
            string genra = null;
            string description = null;
            string length = null;

            if (Title_box.Text != "" &&
            Genra_box.Text != "" &&
            Description_box.Text != "" &&
            Length_box.Text != "")
            {
                ErrorFeild_label.Visibility = Visibility.Hidden;

                title = Title_box.Text;
                genra = Genra_box.Text;
                description = Description_box.Text;
                length = Length_box.Text;
                
                foreach(Movies movie in movieMenu.MovieBankCopy)
                {
                    id++;
                }
                id ++;
                currentuser.MovieList.Add(new Movies(id, title, genra, description, length));

                Movie_box.Items.Refresh();
                Title_box.Clear();
                Genra_box.Clear();
                Description_box.Clear();
                Length_box.Clear();
            }
            else
            {
                ErrorFeild_label.Visibility = Visibility.Visible;
            }

            MySqlConnection connection = new MySqlConnection(connectionString =
                                                           "SERVER=" + server + ";" +
                                                           "DATABASE=" + database + ";" +
                                                           "UID=" + username + ";" +
                                                           "PASSWORD=" + password + ";");

            connection.Open();
            int user_id = currentuser.Id;
            int movie_id = id;
            string query = "INSERT INTO movies(movie_id, title, description, genra, length) " +
                           "VALUES (@movie_id, @title, @description, @genra, @length);";

            string query1 = "INSERT INTO user_movies_lt(user_id, movie_id) VALUES(@user_id, @movie_id);";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlCommand command1 = new MySqlCommand(query1, connection);

            command.Parameters.AddWithValue("@movie_id", movie_id);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@genra", genra);
            command.Parameters.AddWithValue("@length", length);

            command1.Parameters.AddWithValue("@movie_id", movie_id);
            command1.Parameters.AddWithValue("@user_id", user_id);
            
            command.ExecuteNonQuery();
            command1.ExecuteNonQuery();
            connection.Close();
        }


        
        //Objektet användaren klickar på ska dyka upp i respektive box.
        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Movie_box.Items.Count > -1)
            {
                Save_btn.Visibility = Visibility.Visible;

                var EditItem = Movie_box.SelectedItem;

                Title_box.Text = ((Movies)EditItem).Title;
                Genra_box.Text = ((Movies)EditItem).Genra;
                Description_box.Text = ((Movies)EditItem).Description;
                Length_box.Text = ((Movies)EditItem).Length;
            }
        }

        //Får värdet av valda objektet i listboxen, sedan uppdaterad listan & listboxen.
        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            currentuser = login.GetCurrentUserLogin();
            var selecteditem = Movie_box.SelectedItem;
            int index = currentuser.MovieList.IndexOf((Movies)selecteditem);

            int movie_id = ((Movies)selecteditem).Id;
            string title = Title_box.Text;
            string genra = Genra_box.Text;
            string description = Description_box.Text;
            string length = Length_box.Text;

            if (index != -1)
            {
                currentuser.MovieList[index].Title = Title_box.Text;
                currentuser.MovieList[index].Genra = Genra_box.Text;
                currentuser.MovieList[index].Description = Description_box.Text;
                currentuser.MovieList[index].Length = $"{Length_box.Text} h";

                Movie_box.Items.Refresh();

                MySqlConnection connection = new MySqlConnection(connectionString =
                                                          "SERVER=" + server + ";" +
                                                          "DATABASE=" + database + ";" +
                                                          "UID=" + username + ";" +
                                                          "PASSWORD=" + password + ";");
                connection.Open();

                string query = "UPDATE movies SET title = @title, genra = @genra, Description = @Description, Length = @Length WHERE movie_id = @movie_id ;";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@movie_id", movie_id);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@genra", genra);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@length", length);

                command.ExecuteNonQuery();
                connection.Close();
                Save_btn.Visibility = Visibility.Hidden;

                Title_box.Clear();
                Genra_box.Clear();
                Description_box.Clear();
                Length_box.Clear();
            }
            
        }

        //Length_box accepterar enbart numerics och , 
        //Om inte regex matchar blir e=rtrue och inget händer i textboxen
        private void Length_box_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9,]+$");

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }
        //tar användaren tillbaks till movieMenu
        private void Back_Menu_btn_Click(object sender, RoutedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                movieMenu.Visibility = Visibility.Visible;
                this.Visibility = Visibility.Hidden;
                movieMenu.UserMovies_box.Items.Refresh();
            }
        }
    }
}
