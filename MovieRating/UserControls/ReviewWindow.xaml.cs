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
    /// <summary>
    /// Interaction logic for ReviewWindow.xaml
    /// </summary>
    public partial class ReviewWindow : UserControl
    {
        MovieMenu MovieMenu;
        List<Review> ReviewList = new List<Review>();

        public ReviewWindow()
        {
            InitializeComponent();
        }

        public void SetMovieMenu(MovieMenu movieMenu)
        {
            this.MovieMenu = movieMenu;
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string review = null;
            if (Review_box.Text != "")
            {
                review = Review_box.Text;
                Review newReview = new Review(review);
                ReviewList.Add(newReview);
                Review_box.Text = "";

                //sen sql grejs så att det faktiskt läggs till movie_id och DATETIME?
            }   

        }
    }
}
