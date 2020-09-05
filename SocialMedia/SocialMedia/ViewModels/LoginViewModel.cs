using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SocialMedia.Command;
using SocialMedia.Models;
using SocialMedia.Services;
using SocialMedia.Views;

namespace SocialMedia.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        #region Properties

        readonly Login login;

        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private bool isUpdateUser;
        public bool IsUpdateUser
        {
            get
            {
                return isUpdateUser;
            }
            set
            {
                isUpdateUser = value;
            }
        }
        #endregion

        public LoginViewModel(Login login)
        {
            this.login = login;

            user = new tblUser();

            DateTime date = (DateTime)User.DateOfBirth;
            if (date.Year == 1)
            {
                User.DateOfBirth = DateTime.Parse("1990-01-01");
            }
        }

        #region Commands
        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }

        /// <summary>
        /// Exit application
        /// </summary>
        private void ExitExecute()
        {
            MessageBoxResult dialog = Xceed.Wpf.Toolkit.MessageBox.Show("Da li želite napustiti aplikaciju?", "Izlaz", MessageBoxButton.YesNo);

            if (dialog == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private bool CanExitExecute()
        {
            return true;
        }

        /// <summary>
        /// Login user
        /// </summary>
        private ICommand loginUser;
        public ICommand LoginUser
        {
            get
            {
                if (loginUser == null)
                {
                    loginUser = new RelayCommand(param => LoginUserExecute(), param => CanLoginUserExecute());
                }
                return loginUser;
            }
        }

        public static bool usersLogin = false;

        private void LoginUserExecute()
        {
            //try
            //{
            //    LoginService s = new LoginService();

            //    string username = login.NameTextBox.Text;

            //    //uniqueness check username
            //    tblUser usertUsername = s.GetUserUsername(username);

            //    // Hash password
            //    var hasher = new SHA256Managed();
            //    var unhashed = Encoding.Unicode.GetBytes(login.passwordBox.Password);
            //    var hashed = hasher.ComputeHash(unhashed);
            //    var hashedPassword = Convert.ToBase64String(hashed);

            //    string password = hashedPassword;

            //    //Checks if there is a username and password in the database
            //    tblUser userLogin = s.GetUsernamePassword(username, password);

            //    if (login.NameTextBox.Text == "Admin" && login.passwordBox.Password == "Admin123")
            //    {
            //        login.pnlLoginUser.Visibility = Visibility.Collapsed;
            //        login.pnlSuccessfulLogin.Visibility = Visibility.Visible;
            //        LoggedGuest.NameSurname = "Administrator";
            //        LoggedGuest.Username = login.NameTextBox.ToString();
            //        LoggedGuest.ID = 0;
            //        adminLogin = true;
            //        OpenMainMenu();
            //        MessageIngredient();
            //    }
            //    else if (login.NameTextBox.Text.ToLower() == "admin")
            //    {
            //        login.SnackError();
            //        return;
            //    }
            //    else if (userLogin != null)
            //    {
            //        LoggedGuest.NameSurname = userLogin.FirstLastName;
            //        LoggedGuest.Username = userLogin.Username;
            //        LoggedGuest.ID = userLogin.UserID;
            //        usersLogin = true;
            //        login.pnlLoginUser.Visibility = Visibility.Collapsed;
            //        login.pnlSuccessfulLogin.Visibility = Visibility.Visible;
            //        OpenMainMenu();
            //        MessageIngredient();
            //    }
            //    else if (usertUsername != null)
            //    {
            //        login.SnackError();
            //        return;
            //    }
            //    else
            //    {
            //        login.pnlLoginUser.Visibility = Visibility.Collapsed;
            //        login.pnlRegistrationUser.Visibility = Visibility.Visible;
            //        login.nameSurnameUser.Focus();
            //    }
            //}
            //catch (Exception)
            //{

            //}
        }

        private bool CanLoginUserExecute()
        {
            return true;
        }

        /// <summary>
        /// Registration new user
        /// </summary>
        private ICommand registrationUser;
        public ICommand RegistrationUser
        {
            get
            {
                if (registrationUser == null)
                {
                    registrationUser = new RelayCommand(param => RegistrationUserExecute(), param => CanRegistrationUserExecute());
                }
                return registrationUser;
            }
        }

        public async void OpenMainMenu()
        {
            await Task.Delay(1000);
            login.Close();
        }

        private void RegistrationUserExecute()
        {

            try
            {
                Service s = new Service();
                // Hash Password
                var hasher = new SHA256Managed();
                var unhashed = Encoding.Unicode.GetBytes(login.txtPassword.Password.ToString());
                var hashed = hasher.ComputeHash(unhashed);
                var hashedPassword = Convert.ToBase64String(hashed);
                this.User.UserPassword = hashedPassword;
                this.User.UserName = login.txtName.Text.ToString();
                this.User.UserSurname = login.txtSurname.Text.ToString();
                this.User.UserUsername = login.txtUsernameRegistration.Text.ToString();
                this.User.Gender = login.cbxGendre.Text.ToString();
                this.User.VisibleDate = 1;
                this.User.VisibleGender = 1;

                if (user.Gender == "")
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Odaberite pol.", "Pol");
                    return;
                }
                
                string usernamer = login.txtUsernameRegistration.Text;
                
                
                    tblUser userr= s.GetUserUsername(usernamer);

                    if (userr != null)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Korisničko ime je zauzeto, probajte neko drugo.", "Korisničko ime je zauzeto");
                        return;
                    }
                   

                int age = 0;
                age = DateTime.Now.Year - User.DateOfBirth.Year;
                if (DateTime.Now.DayOfYear < User.DateOfBirth.DayOfYear)
                {
                    age = age - 1;
                }
                if (age < 18)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Nije moguće registrovati nalog za osobe mlađe od 18 godina.", "Morate imati minimum 18 godina");
                    return;
                }


                if (s.AddUser(User) != null)
                {
                    IsUpdateUser = true;
                    usersLogin = true;
                    login.txtName.Text = "";
                    login.txtSurname.Text = "";
                    login.txtUsernameRegistration.Text = "";
                    login.txtPasswordRegistration.Password = "";
                    login.dpDateOfBirth.Text = "";
                    login.cbxGendre.Text = "";
                    var drawer = DrawerHost.CloseDrawerCommand;
                    drawer.Execute(null, null);
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
            }

        }
            

        private bool CanRegistrationUserExecute()
        {
            return true;
        }

        public async void VisibleLoginFail()
        {
            //login.loginFail.Visibility = Visibility.Visible;
            //await Task.Delay(2000);
            //login.loginFail.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
