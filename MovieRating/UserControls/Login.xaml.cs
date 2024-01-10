using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    
    public partial class Login : UserControl
    {
        Register register;
        MovieManager movieManager;
        MovieMenu movieMenu;
        User currentUser = null;

        public Login()
        {
            InitializeComponent();
        }

        //sätter värdet på register
        public void SetWindows(Register register,MovieManager movieManager, MovieMenu movieMenu)
        {
            this.movieManager = movieManager;
            this.register = register;
            this.movieMenu = movieMenu;
        }

        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginUser();
            Username_box.Clear();
            Password_box.Clear();

        }
        //visar registrerings fönstret,tar bort error meddelanden
        private void ToRegister_btn_Click(object sender, RoutedEventArgs e)
        {
            register.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
            UserError_label.Visibility = Visibility.Hidden;
        }

        //kontrollerar om det finns en registrerad användare, om det gör->logga in.
        //Om inte -> errorMeddelande.
        private void LoginUser()
        {
            if (register.UserList.Count >0)
            {
                foreach (User user in register.UserList)
                {
                    if (Username_box.Text.ToLower() == user.UserName.ToLower() &&
                       Password_box.Password == user.Password)
                    {
                        UserError_label.Visibility = Visibility.Hidden;
                        this.Visibility = Visibility.Hidden;
                        currentUser = user;
                        movieManager.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Password_box.Clear();
                        UserError_label.Visibility= Visibility.Visible;
                    }
                }
            }
            else
            {
                UserError_label.Visibility = Visibility.Visible;
            }
        }
        //Ger tillgång till den inloggade användaren till resten av programmet
        public User GetLogedInUser()
        {
            return currentUser;
        }
    }
}

