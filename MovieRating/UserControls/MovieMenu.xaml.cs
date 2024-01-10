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

namespace MovieRating.UserControls
{
    /// <summary>
    /// Interaction logic for MovieMenu.xaml
    /// </summary>
    public partial class MovieMenu : UserControl
    {
        Login login;
        User currentuser = null;
        List<Movies> MovieBank = new List<Movies>();
        MovieManager movieManager;

        public MovieMenu()
        {
            InitializeComponent();
            LoadData();
        }

        //sättervärdet av login
        public void SetWindows(Login login, MovieManager movieManager)
        {
            this.login = login;
            this.movieManager = movieManager;
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
            currentuser = login.GetLogedInUser();
            var EditItem = MovieBank_box.SelectedItem;

            if (MovieBank_box.Items.Count > -1 && 
                !UserMovies_box.SelectedItems.Contains(EditItem))
            {
                Error_label.Visibility = Visibility.Visible;
                currentuser.MovieList.Add((Movies)EditItem);

                UserMovies_box.ItemsSource = currentuser.MovieList;
                UserMovies_box.Items.Refresh();
            }
            else if(UserMovies_box.SelectedItems.Contains(EditItem))
            {
                Error_label.Visibility = Visibility.Visible;
            }
        }

        //Tar bort valda indexet i listboxen av filmer använadren samlat ihop
        private void RemoveMovie()
        {
            currentuser = login.GetLogedInUser();
            var EditItem = UserMovies_box.SelectedItem;

            if (UserMovies_box.Items.Count > -1)
            {
                currentuser.MovieList.Remove((Movies)EditItem);
                UserMovies_box.Items.Refresh();
            }
        }


        //Läser in datan från databasen
        private void LoadData()
        {
            //
            //ska fyllas med skrips till databasen
            //
            MovieBank_box.ItemsSource = MovieBank;
        }



        private void FilterAÖ_Click(object sender, RoutedEventArgs e)
        {
            //ska fyllas med skrips till databasen
        }

        private void RatingU5_Click(object sender, RoutedEventArgs e)
        {
            //ska fyllas med skrips till databasen
        }

        private void LengtBtn_Click(object sender, RoutedEventArgs e)
        {
            //ska fyllas med skrips till databasen
        }

        //Tar användaren till MovieManager där man kan skapa en obefintlig film
        private void CreateMovie_btn_Click(object sender, RoutedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                movieManager.Visibility = Visibility.Visible;
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}
