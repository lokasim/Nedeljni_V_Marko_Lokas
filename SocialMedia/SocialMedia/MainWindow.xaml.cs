using SocialMedia.ViewModels;
using SocialMedia.Views;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if(MoreInformation.Visibility == Visibility.Collapsed)
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
    }
}
