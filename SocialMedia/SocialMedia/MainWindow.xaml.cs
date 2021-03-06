﻿using SocialMedia.ViewModels;
using SocialMedia.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace SocialMedia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWidnowViewModel(this);
            this.Language = XmlLanguage.GetLanguage("sr-SR");

            Login login = new Login();
            if (login.loggedIn == false)
            {
                login.ShowDialog();
            }
            else
            {
                login.Close();
            }
            PrintInformation();
        }

        private void PrintInformation()
        {
            tblName.Text = LoggedGuest.Name;
            tblSurname.Text = LoggedGuest.Surname;
            tblUsername.Text = LoggedGuest.Username;
            tblDatum.Text = LoggedGuest.Birth;
            tblPol.Text = LoggedGuest.Gendre;
            char[] first = LoggedGuest.Name.ToCharArray();
            char[] second = LoggedGuest.Surname.ToCharArray();
            string initialText = $"{second[0].ToString().ToUpper()}{first[0].ToString().ToUpper()}";
            initials.Text = initialText;
        }

        private void DragMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dijalog = Xceed.Wpf.Toolkit.MessageBox.Show("Da li želite da napustite aplikaciju?", "Izlaz", MessageBoxButton.YesNo);

            if (dijalog == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Window enlargement method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Povecaj_prozor(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                PovecajProzor.ToolTip = "Smanji prozor";
                PovecajProzor1.Visibility = Visibility.Visible;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                PovecajProzor.ToolTip = "Povećaj prozor";
                PovecajProzor1.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Window reduction method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Spusti_prozor(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            else if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void BtnSeeMore_Click(object sender, RoutedEventArgs e)
        {
            if (MoreInformation.Visibility == Visibility.Collapsed)
            {
                MoreInformation.Visibility = Visibility.Visible;
                btnSeeMore.Visibility = Visibility.Collapsed;
                btnSeeLess.Visibility = Visibility.Visible;
            }
            else
            {
                btnSeeMore.Visibility = Visibility.Visible;
                btnSeeLess.Visibility = Visibility.Collapsed;
                MoreInformation.Visibility = Visibility.Collapsed;
            }
        }

        private void CbxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSearch.SelectedIndex.ToString() == "0".ToString())
            {
                usernamePnl.Visibility = Visibility.Collapsed;
                imeprezimepnl.Visibility = Visibility.Visible;
            }
            else if (cbxSearch.SelectedIndex.ToString() == "1".ToString())
            {

                usernamePnl.Visibility = Visibility.Visible;
                imeprezimepnl.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginViewModel.usersLogin = false;
            this.Close();
            Login login = new Login
            {
                loggedIn = false
            };

            MainWindow main = new MainWindow();
            main.ShowDialog();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            btnHomeKind.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
            btnProfileKind.Foreground = new SolidColorBrush(Colors.White);
            btnSearchKind.Foreground = new SolidColorBrush(Colors.White);
            btnFriendRequestKind.Foreground = new SolidColorBrush(Colors.White);
            FriendRequestPanel.Visibility = Visibility.Collapsed;
            HomePanel.Visibility = Visibility.Visible;
            SearchFriendPanel.Visibility = Visibility.Collapsed;
            txtUsername.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            cbxSearch.Text = "";

        }

        private void BtnFriendRequest_Click(object sender, RoutedEventArgs e)
        {
            btnFriendRequestKind.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
            btnProfileKind.Foreground = new SolidColorBrush(Colors.White);
            btnSearchKind.Foreground = new SolidColorBrush(Colors.White);
            btnHomeKind.Foreground = new SolidColorBrush(Colors.White);
            FriendRequestPanel.Visibility = Visibility.Visible;
            HomePanel.Visibility = Visibility.Collapsed;
            SearchFriendPanel.Visibility = Visibility.Collapsed;
            txtUsername.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            cbxSearch.Text = "";
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            btnProfileKind.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
            btnHomeKind.Foreground = new SolidColorBrush(Colors.White);
            btnSearchKind.Foreground = new SolidColorBrush(Colors.White);
            btnFriendRequestKind.Foreground = new SolidColorBrush(Colors.White);
            FriendRequestPanel.Visibility = Visibility.Collapsed;
            HomePanel.Visibility = Visibility.Visible;
            SearchFriendPanel.Visibility = Visibility.Collapsed;
            txtUsername.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            cbxSearch.Text = "";
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            btnSearchKind.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
            btnProfileKind.Foreground = new SolidColorBrush(Colors.White);
            btnHomeKind.Foreground = new SolidColorBrush(Colors.White);
            btnFriendRequestKind.Foreground = new SolidColorBrush(Colors.White);
            FriendRequestPanel.Visibility = Visibility.Collapsed;
            HomePanel.Visibility = Visibility.Collapsed;
            SearchFriendPanel.Visibility = Visibility.Visible;

        }

        private void BtnLike_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnUnLike_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Flipper_IsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {

        }
    }
}
