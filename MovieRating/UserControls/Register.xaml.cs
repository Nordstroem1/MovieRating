using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        Login login;

        public Register()
        {
            InitializeComponent();
        }

        public List<User> UserList = new List<User>();

        //sätter värdet på Login.
        public void SetLogin(Login login)
        {
            this.login = login;
        }

        //Lägger till användare om lösenorden matchar och användaren inte finns i listan.
        private void CreateUser()
        {

            string name = User_box.Text;
            string password = Psw_box.Password;
            string repeatPsw = Repeat_Box.Password;

            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].UserName.ToLower() == name.ToLower())
                {
                    Error_label.Visibility = Visibility.Visible;
                    return;
                }
            }

            if (password.ToLower() != repeatPsw.ToLower())
            {
                Password_label.Visibility = Visibility.Visible;
            }
            else
            {
                UserList.Add(new User(name, password));
                User_box.Clear();
                Psw_box.Clear ();
                Repeat_Box.Clear ();
            }
        }

        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateUser();
            this.Visibility = Visibility.Hidden;
            login.Visibility = Visibility.Visible;
        }

        private void GoBack_btm_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            login.Visibility = Visibility.Visible;
        }
    }
}

