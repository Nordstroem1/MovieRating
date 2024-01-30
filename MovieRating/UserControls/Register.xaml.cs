using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        Login login;
        public List<User> UserList = new List<User>();

        bool RegisterOK = false;
        string name;
        string user_password;

        
        public Register()
        {
            InitializeComponent();
        }

        

        //sätter värdet på Login.
        public void SetLogin(Login login)
        {
            this.login = login;
        }

        //Lägger till användare om lösenorden matchar och användaren inte finns i listan.
        private void CreateUser()
        {

            name = User_box.Text;
            user_password = Psw_box.Password;
            string repeatPsw = Repeat_Box.Password;
            int nextID = 0;

            //Användarnamn måste var längre än 5 tecken
            Regex regex = new Regex(@"^[a-zA-Z0-9]{6,}$");

            if (regex.IsMatch(name))
            {
                Lbl_UsrN_error.Visibility = Visibility.Hidden;
                RegisterOK = true;

                for (int i = 0; i < UserList.Count; i++)
                {
                    if (UserList[i].UserName.ToLower() == name.ToLower())
                    {
                        Error_label.Visibility = Visibility.Visible;
                        return;
                    }
                }

                if (user_password.ToLower() != repeatPsw.ToLower())
                {
                    Password_label.Visibility = Visibility.Visible;
                }
                else
                {
                    nextID = GethighestUserId();
                    UserList.Add(new User(nextID,name, user_password));
                    AddUsersToDB();
                    User_box.Clear();
                    Psw_box.Clear();
                    Repeat_Box.Clear();
                }
            }
            else
            {
                Lbl_UsrN_error.Visibility = Visibility.Visible;
                RegisterOK = false;
            }
        }
        //hämtar och lägger till
        private int GethighestUserId()
        {
            int nextid = 0;
            foreach(User user in UserList)
            {
                nextid++;
            }
            return nextid;
        }

        //om regexen är korrekt kommer användaren till Login annars är den kvar.
        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateUser();
            if (RegisterOK == true)
            {
                this.Visibility = Visibility.Hidden;
                login.Visibility = Visibility.Visible;
            }
        }


        private void GoBack_btm_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            login.Visibility = Visibility.Visible;

        }


        private void AddUsersToDB()
        {
            string server = "localhost";
            string database = "MovieRating";
            string username = "root";
            string password = "";
            string connectionString = "";


            //ansluter till databasen
            MySqlConnection connection = new MySqlConnection(connectionString =
                                                            "SERVER=" + server + ";" +
                                                            "DATABASE=" + database + ";" +
                                                            "UID=" + username + ";" +
                                                            "PASSWORD=" + password + ";");

            connection.Open();

            string query1 = "SELECT COUNT(*) FROM users WHERE username = @username;";

            //Kör kommandot "queryn" vi skickade in, i mysql
            MySqlCommand command = new MySqlCommand(query1, connection);

            //ger Username värdet av inputen som användaren gav
            command.Parameters.AddWithValue("@username", name);

            //om count är större än 0 så finns användaren registerad.
            int count = Convert.ToInt32(command.ExecuteNonQuery());

            if (count > 0)
            {
                Error_label.Visibility = Visibility.Visible;
            }
            else
            {
                command.Parameters.AddWithValue("@PASSWORD", user_password);
                //insert_user = stored procedure
                string query2 = "CALL insert_user(@username,@PASSWORD);";
                command.CommandText = query2;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        //hämtar alla användare från Databasen.
        public void GetUserFromDb()
        {
            string server = "localhost";
            string database = "MovieRating";
            string username = "root";
            string password = "";
            string connectionString = "";

            MySqlConnection connection = new MySqlConnection(connectionString =
                                                            "SERVER=" + server + ";" +
                                                            "DATABASE=" + database + ";" +
                                                            "UID=" + username + ";" +
                                                            "PASSWORD=" + password + ";");

            connection.Open();

            string query1 = "SELECT * FROM users;";
            
            MySqlCommand command = new MySqlCommand(query1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (!UserList.Contains(reader["username"]))
                {
                    User user = new User((int)reader["user_id"],
                                         (string)reader["username"],
                                         (string)reader["PASSWORD"]); 

                    UserList.Add(user);
                }
            }
        }

    }
}

