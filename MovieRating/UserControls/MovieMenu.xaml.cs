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
        List<Movies> MovieBankCopy = new List<Movies>();
        MovieManager movieManager;
        ReviewWindow ReviewWindow;

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
        public void LoadData()
        {
            DataBaseConnection db = new DataBaseConnection();
            MovieBankCopy = db.GetAllMovies();
            MovieBank_box.ItemsSource = MovieBankCopy;
        }



        private void FilterAÖ_Click(object sender, RoutedEventArgs e)
        {
            //ska fyllas med skrips till databasen
        }

        private void Drama_movies_Click(object sender, RoutedEventArgs e)
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



        private void Review_btn_Click(object sender, RoutedEventArgs e)
        {
            var chosenMovie = UserMovies_box.SelectedItem;

            if (UserMovies_box.Items.Count > -1||
                MovieBank_box.Items.Count > -1)
            {
                ReviewWindow.Visibility = Visibility.Visible;
            }
        }

        private void Read_review_btn_Click(object sender, RoutedEventArgs e)
        {
            var chosenMovie = UserMovies_box.SelectedItem;

            if (UserMovies_box.Items.Count > -1)
            {
                ReviewWindow.Visibility= Visibility.Visible;

                //ReviewWindow.Review_box.Text = 
                //hur läser jag in rviewen? är de från sql man hittar den filmen med rätt review_id och samma movie_id? hur funkar det? 

            }
        }
        

    }
}
