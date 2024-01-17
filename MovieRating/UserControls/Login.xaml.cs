using MySql.Data.MySqlClient;
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
using System.Xml.Linq;

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
            register.GetUserFromDb();
            if (CheckUsers())
            {
                UserError_label.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Hidden;
                currentUser = GetCurrentUserLogin();
                movieMenu.Visibility = Visibility.Visible;
                movieMenu.LoadMoviesFromDB();
            }
            else
            {
                Password_box.Clear();
                UserError_label.Visibility = Visibility.Visible;
            }
            
        }
        
        //Kollar om användaren med lösenord finns i databasenm om det gör det = true
        private bool CheckUsers()
        {
            string server = "localhost";
            string database = "MovieRating";
            string username = "root";
            string password = "Ktmpappa#27";
            string connectionString = "";

            MySqlConnection connection = new MySqlConnection(connectionString =
                                                            "SERVER=" + server + ";" +
                                                            "DATABASE=" + database + ";" +
                                                            "UID=" + username + ";" +
                                                            "PASSWORD=" + password + ";");

            connection.Open();            
            
            string query = "SELECT * FROM users WHERE username = @username AND password = @password;";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", Username_box.Text);
            command.Parameters.AddWithValue("@password", Password_box.Password);

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
       // hämtar den inloggade användaren
        public User GetCurrentUserLogin()
        {
            foreach(User user in register.UserList)
            {
                if(Username_box.Text == user.UserName)
                {
                    currentUser = user;
                }
            }
            return currentUser;
        }
    }
}

