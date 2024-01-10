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

namespace MovieRating.UserControls
{
    public partial class MovieManager : UserControl
    {
        //nu skaps listan här och i user///
        
        Login login;
        MovieMenu movieMenu;
        User currentuser = null;


        public MovieManager()
        {
            InitializeComponent();
        }

        
        //Sätter värdet av login.
        public void SetLogin(Login login)
        {
            this.login = login;
        }
        //Sätter värdet av Moviemenu.
        public void SetMovieMenu(MovieMenu movieMenu)
        {
            this.movieMenu = movieMenu;
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

        //lägger till en film i MovieList
        private void AddMovies()
        {
            //funkar detta?????+ verkar så när jag debuggar???? smidigare sätt????
            currentuser = login.GetLogedInUser();
            
            if (Title_box.Text != "" &&
            Genra_box.Text != "" &&
            Description_box.Text != "" &&
            Length_box.Text != "")
            {
                ErrorFeild_label.Visibility = Visibility.Hidden;

                string title = Title_box.Text;
                string genra = Genra_box.Text;
                string description = Description_box.Text;
                string length = Length_box.Text;
                string rating = ratingslider.Value.ToString();

                currentuser.MovieList.Add(new Movies(title, genra, description, length, rating));

                //sorterar listan, ger listboxen värdet av movielist
                currentuser.MovieList.Sort((x, y) => y.Rating.CompareTo(x.Rating));

                Movie_box.ItemsSource = currentuser.MovieList;
                Movie_box.Items.Refresh();

                Title_box.Clear();
                Genra_box.Clear();
                Description_box.Clear();
                Length_box.Clear();
                ratingslider.Value = 0;
            }
            else
            {
                ErrorFeild_label.Visibility = Visibility.Visible;
            }
        }

        //Objektet användaren klickar på ska dyka upp i respektive box/slider.
        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Movie_box.Items.Count > -1)
            {
                Save_btn.Visibility = Visibility.Visible;

                var EditItem = Movie_box.SelectedItem;
                string slider = ratingslider.Value.ToString();

                Title_box.Text = ((Movies)EditItem).Title;
                Genra_box.Text = ((Movies)EditItem).Genra;
                Description_box.Text = ((Movies)EditItem).Description;
                Length_box.Text = ((Movies)EditItem).Length;
                slider = ((Movies)EditItem).Rating;
            }
        }

        //Får värdet av valda objektet i listboxen, sedan uppdaterad listan & listboxen.
        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            var selecteditem = Movie_box.SelectedItem;
            int index = currentuser.MovieList.IndexOf((Movies)selecteditem);

            if (index != -1)
            {
                currentuser.MovieList[index].Title = Title_box.Text;
                currentuser.MovieList[index].Genra = Genra_box.Text;
                currentuser.MovieList[index].Description = Description_box.Text;
                currentuser.MovieList[index].Length = $"{Length_box.Text} h";
                currentuser.MovieList[index].Rating = $"Rating: {ratingslider.Value}";

                Movie_box.Items.Refresh();
                Save_btn.Visibility = Visibility.Hidden;

                Title_box.Text = "";
                Genra_box.Text = "";
                Description_box.Text = "";
                Length_box.Text = "";
                ratingslider.Value = 0;
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
