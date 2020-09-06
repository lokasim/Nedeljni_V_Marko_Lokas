using MaterialDesignThemes.Wpf;
using SocialMedia.ViewModels;
using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace SocialMedia.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public bool loggedIn;

        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this);
            this.Language = XmlLanguage.GetLanguage("sr-SR");
            btnRegistration.IsEnabled = true;
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
        public bool username;
        public bool password;

        private void TxtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUsername.Text.Length <= 5)
            {
                txtUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                txtUsername.Foreground = new SolidColorBrush(Colors.Red);
                username = false;
            }
            else
            {
                txtUsername.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                txtUsername.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                username = true;
            }
            LoginOk();
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.Length <= 5)
            {
                txtPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPassword.Foreground = new SolidColorBrush(Colors.Red);
                password = false;
            }
            else
            {
                txtPassword.BorderBrush = new SolidColorBrush(Color.FromRgb(0,53,133));
                txtPassword.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                password = true;
            }
            LoginOk();
        }

        private void LoginOk()
        {
            if (password == true && username == true)
            {
                btnLogin.IsEnabled = true;
            }
            else
            {
                btnLogin.IsEnabled = false;
            }
        }

        private void TxtBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
            if (e.Key == Key.Space)
            {
                SystemSounds.Beep.Play();
            }
        }

        private Boolean TextAllowedMalaSlova(String s)
        {
            foreach (Char c in s.ToCharArray())
            {
                if (Char.IsLower(c) || Char.IsDigit(c) || Char.IsControl(c))
                {
                    tbCapsLock.Visibility = Visibility.Collapsed;
                    continue;
                }
                else
                {
                    tbCapsLock.Visibility = Visibility.Visible;
                    tbCapsLock.Text = "Dozvoljn je unos malih slova i brojeva";
                    SystemSounds.Beep.Play();
                    return false;
                }
            }
            return true;
        }

        //samo mala slova i brojevi
        private void PreviewTextInputHandlerMala(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextAllowedMalaSlova(e.Text);
        }

        private Boolean TextAllowedVelikaSlova(String s)
        {
            foreach (Char c in s.ToCharArray())
            {
                if (Char.IsLower(c) || Char.IsUpper(c) || Char.IsDigit(c) || Char.IsControl(c))
                {
                    tbCapsLock.Visibility = Visibility.Collapsed;
                    continue;
                }
                else
                {
                    tbCapsLock.Visibility = Visibility.Visible;
                    tbCapsLock.Text = "Dozvoljne je unos slova i brojeva";
                    SystemSounds.Beep.Play();
                    return false;
                }
            }
            return true;
        }

        //samo mala slova i brojevi
        private void PreviewTextInputHandlerVelika(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextAllowedVelikaSlova(e.Text);
        }

        private Boolean TextAllowed(String s)
        {
            foreach (Char c in s.ToCharArray())
            {
                if (Char.IsLetter(c) || Char.IsControl(c))
                {

                    continue;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    return false;
                }
            }
            return true;
        }

        private void PreviewTextInputHandler(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextAllowed(e.Text);
        }

        // banned pasting value
        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            String s = (String)e.DataObject.GetData(typeof(String));
            if (!TextAllowed(s)) e.CancelCommand();
        }

        private Boolean NumberAllowed(String s)
        {
            foreach (Char c in s.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c))
                {

                    continue;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    return false;
                }
            }
            return true;
        }

        //only numbers
        private void PreviewNumberInputHandler(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumberAllowed(e.Text);
        }


        public bool usernameRegistration;
        public bool passwordRegistration;
        public bool name;
        public bool surname;
        public bool date;
        public bool gendre;

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length <= 2)
            {
                txtName.BorderBrush = new SolidColorBrush(Colors.Red);
                txtName.Foreground = new SolidColorBrush(Colors.Red);
                name = false;
            }
            else
            {
                txtName.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                txtName.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                name = true;
            }
            RegistrationOk();
        }

        private void TxtSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSurname.Text.Length <= 2)
            {
                txtSurname.BorderBrush = new SolidColorBrush(Colors.Red);
                txtSurname.Foreground = new SolidColorBrush(Colors.Red);
                surname = false;
            }
            else
            {
                txtSurname.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                txtSurname.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                surname = true;
            }
            RegistrationOk();
        }

        private void TxtUsernameRegistration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUsernameRegistration.Text.Length <= 5)
            {
                txtUsernameRegistration.BorderBrush = new SolidColorBrush(Colors.Red);
                txtUsernameRegistration.Foreground = new SolidColorBrush(Colors.Red);
                usernameRegistration = false;
            }
            else
            {
                txtUsernameRegistration.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                txtUsernameRegistration.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                usernameRegistration = true;
            }
            RegistrationOk();
        }

        private void TxtPasswordRegistration_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPasswordRegistration.Password.Length <= 5)
            {
                txtPasswordRegistration.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPasswordRegistration.Foreground = new SolidColorBrush(Colors.Red);
                passwordRegistration = false;
            }
            else
            {
                txtPasswordRegistration.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                txtPasswordRegistration.Foreground = new SolidColorBrush(Color.FromRgb(0, 53, 133));
                passwordRegistration = true;
            }
            RegistrationOk();
        }
        
        private void DpDateOfBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpDateOfBirth.Text.ToString() == "")
            {
                date = false;
            }
            else
            {
                date = true;
            }
            RegistrationOk();
        }

        private void CbxGendre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxGendre.Text.ToString() == "")
            {
                gendre = false;
            }
            else
            {
                gendre = true;
            }
            RegistrationOk();
        }

        public void CloseDialog()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtUsernameRegistration.Text = "";
            txtPasswordRegistration.Password = "";
            cbxGendre.Text = "";
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void RegistrationOk()
        {
            
            if (usernameRegistration == true &&
                passwordRegistration == true &&
                name == true &&
                surname == true &&
                date == true)
            {
                btnRegistration.IsEnabled = true;
            }
            else
            {
                btnRegistration.IsEnabled = false;
            }
        }

        
    }
}
