using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
            try
            {

                Service s = new Service();

                string username = login.txtUsername.Text;


                // Hash password
                var hasher = new SHA256Managed();
                var unhashed = Encoding.Unicode.GetBytes(login.txtPassword.Password);
                var hashed = hasher.ComputeHash(unhashed);
                var hashedPassword = Convert.ToBase64String(hashed);

                string password = hashedPassword;

                //Checks if there is a username and password in the database
                tblUser userLogin = s.GetUsernamePassword(username, password);

                if (userLogin != null)
                {
                    LoggedGuest.Name = userLogin.UserName;
                    LoggedGuest.Surname = userLogin.UserSurname;
                    LoggedGuest.Username = userLogin.UserUsername;
                    LoggedGuest.Birth = userLogin.DateOfBirth.ToString("dd.MM.yyyy");
                    LoggedGuest.Gendre = userLogin.Gender;
                    LoggedGuest.ID = userLogin.UserID;
                    usersLogin = true;
                    OpenMainMenu();
                }
                else
                {
                    SnackError();
                }
            }
            catch (Exception)
            {

            }
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
            await Task.Delay(250);
            login.Close();
        }

        private void RegistrationUserExecute()
        {
            try
            {
                Service s = new Service();
                // Hash Password
                var hasher = new SHA256Managed();
                var unhashed = Encoding.Unicode.GetBytes(login.txtPasswordRegistration.Password.ToString());
                var hashed = hasher.ComputeHash(unhashed);
                var hashedPassword = Convert.ToBase64String(hashed);

                string password = hashedPassword;
                this.User.UserPassword = password;
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


                tblUser userr = s.GetUserUsername(usernamer);

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
                    Xceed.Wpf.Toolkit.MessageBox.Show("Nije moguće registrovati nalog za osobe mlađe od 18 godina.", "Minimum 18 godina");
                    return;
                }


                if (s.AddUser(User) != null)
                {
                    IsUpdateUser = true;
                    login.CloseDialog();
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

        private ICommand backToLogin;
        public ICommand BackToLogin
        {
            get
            {
                if (backToLogin == null)
                {
                    backToLogin = new RelayCommand(param => BackLoginExecute(), param => CanBackLoginExecute());
                }
                return backToLogin;
            }
        }

        //Return to the login page
        private void BackLoginExecute()
        {
            login.CloseDialog();

        }
        private bool CanBackLoginExecute()
        {
            return true;
        }

        public async void SnackError()
        {
            login.SnackErrorSNC.IsActive = true;
            await Task.Delay(3000);
            login.SnackErrorSNC.IsActive = false;
        }
        #endregion
    }
}
