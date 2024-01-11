﻿using System.Windows;

namespace MovieRating
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            login.SetWindows(register,moviemanager,moviemenu);
            register.SetLogin(login);
            moviemanager.SetLogin(login);
            moviemanager.SetMovieMenu(moviemenu);
            moviemenu.SetWindows(login,moviemanager,reviewwindow);
            reviewwindow.SetMovieMenu(moviemenu);
            
        }
    }
}