using SocialMedia.Command;
using SocialMedia.Models;
using SocialMedia.Services;
using SocialMedia.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SocialMedia.ViewModels
{
    class MainWidnowViewModel : ViewModelBase
    {
        readonly MainWindow mainWindow;
        List<int> listFr = new List<int>();

        private List<tblUser> friendList;
        public List<tblUser> FriendList
        {
            get
            {
                return friendList;
            }
            set
            {
                friendList = value;
                OnPropertyChanged("FriendList");
            }
        }

        private List<tblUser> userListSearch;
        public List<tblUser> UserListSearch
        {
            get
            {
                return userListSearch;
            }
            set
            {
                userListSearch = value;
                OnPropertyChanged("UserListSearch");
            }
        }

        private List<tblUser> userListSearchOther;
        public List<tblUser> UserListSearchOther
        {
            get
            {
                return userListSearchOther;
            }
            set
            {
                userListSearchOther = value;
                OnPropertyChanged("UserListSearchOther");
            }
        }

        private tblPost post;
        public tblPost Post
        {
            get
            {
                return post;
            }
            set
            {
                post = value;
                OnPropertyChanged("Post");
            }
        }

        private List<Tuple<string, string, string, string, DateTime>> postList;
        public List<Tuple<string, string, string, string, DateTime>> PostList
        {
            get
            {
                return postList;
            }
            set
            {
                postList = value;
                OnPropertyChanged("PostList");
            }
        }

        Login login = new Login();

        public MainWidnowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            FriendListChat();
            PostPost();
            Post = new tblPost();
        }

        public void PostPost()
        {
            Service s = new Service();
            PostList = s.GetAllPost();
            if (PostList.Count > 0)
            {
                mainWindow.NoPost.Visibility = Visibility.Collapsed;
                mainWindow.ListviewPost.Visibility = Visibility.Visible;
            }
            else
            {
                mainWindow.NoPost.Visibility = Visibility.Visible;
                mainWindow.ListviewPost.Visibility = Visibility.Collapsed;
            }
        }

        public void FriendListChat()
        {
            Service s = new Service();

            try
            {
                FriendList = s.GetAllUser(LoggedGuest.ID);
                if (friendList.Count == 1)
                {
                    mainWindow.ExpanderFriends.Header = $"Ćaskanje ({FriendList.Count()} prijatelj)";

                }
                else
                {
                    mainWindow.ExpanderFriends.Header = $"Ćaskanje ({FriendList.Count()} prijatelja)";

                }

            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
            }
        }

        private ICommand searchBtn;
        public ICommand SearchBtn
        {
            get
            {
                if (searchBtn == null)
                {
                    searchBtn = new RelayCommand(param => SearchBtnExecute(), param => CanSearchBtnExecute());
                }
                return searchBtn;
            }
        }

        //Search page
        private void SearchBtnExecute()
        {
            Service s = new Service();



            if (mainWindow.cbxSearch.Text == "Ime i prezime" || mainWindow.cbxSearch.Text == "")
            {
                if (mainWindow.txtName.Text != "" && mainWindow.txtSurname.Text != "")
                {
                    UserListSearch = s.GetAllUserNameSurname(mainWindow.txtName.Text, mainWindow.txtSurname.Text);
                    UserListSearchOther = s.GetAllUserNameSurnameOther(mainWindow.txtName.Text, mainWindow.txtSurname.Text);
                    mainWindow.SearchDetail.Text = $"Ime: {mainWindow.txtName.Text} Prezime: {mainWindow.txtSurname.Text}";
                }
                else if (mainWindow.txtName.Text != "" && mainWindow.txtSurname.Text == "")
                {
                    UserListSearch = s.GetAllUserName(mainWindow.txtName.Text);
                    UserListSearchOther = s.GetAllUserNameOther(mainWindow.txtName.Text);
                    mainWindow.SearchDetail.Text = $"Ime: {mainWindow.txtName.Text}";

                }
                else if (mainWindow.txtName.Text == "" && mainWindow.txtSurname.Text != "")
                {
                    UserListSearch = s.GetAllUserSurname(mainWindow.txtSurname.Text);
                    UserListSearchOther = s.GetAllUserSurnameOther(mainWindow.txtSurname.Text);
                    mainWindow.SearchDetail.Text = $"Prezime: {mainWindow.txtSurname.Text}";

                }
            }
            else if (mainWindow.cbxSearch.Text == "Korisničko ime")
            {
                UserListSearch = s.GetAllUserUsername(mainWindow.txtUsername.Text);
                UserListSearchOther = s.GetAllUserUsernameOther(mainWindow.txtUsername.Text);
                mainWindow.SearchDetail.Text = $"Korisničko ime: {mainWindow.txtUsername.Text}";

            }



            if (UserListSearch.Count == 0)
            {
                mainWindow.SearchNoData.Visibility = Visibility.Visible;
            }
            else
            {
                mainWindow.SearchNoData.Visibility = Visibility.Collapsed;

            }
        }
        private bool CanSearchBtnExecute()
        {
            if (mainWindow.txtUsername.Text != "" || mainWindow.txtName.Text != "" || mainWindow.txtSurname.Text != "")
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        private ICommand friendRequestBtn;
        public ICommand FriendRequestBtn
        {
            get
            {
                if (friendRequestBtn == null)
                {
                    friendRequestBtn = new RelayCommand(param => FriendRequestBtnExecute(), param => CanFriendRequestBtnExecute());
                }
                return friendRequestBtn;
            }
        }

        //My Prfile page
        private void FriendRequestBtnExecute()
        {
        }
        private bool CanFriendRequestBtnExecute()
        {
            return true;
        }



        private ICommand homeBtn;
        public ICommand HomeBtn
        {
            get
            {
                if (homeBtn == null)
                {
                    homeBtn = new RelayCommand(param => HomeExecute(), param => CanHomeExecute());
                }
                return homeBtn;
            }
        }

        //My Prfile page
        private void HomeExecute()
        {
            PostPost();
        }
        private bool CanHomeExecute()
        {
            return true;
        }

        private ICommand myProfileBtn;
        public ICommand MyProfileBtn
        {
            get
            {
                if (myProfileBtn == null)
                {
                    myProfileBtn = new RelayCommand(param => MyProfileExecute(), param => CanMyProfileExecute());
                }
                return myProfileBtn;
            }
        }

        //My Prfile page
        private void MyProfileExecute()
        {
            Service s = new Service();
            PostList = s.GetMyPost(LoggedGuest.ID);
            if (PostList.Count > 0)
            {
                mainWindow.NoPost.Visibility = Visibility.Collapsed;
                mainWindow.ListviewPost.Visibility = Visibility.Visible;
            }
            else
            {
                mainWindow.NoPost.Visibility = Visibility.Visible;
                mainWindow.ListviewPost.Visibility = Visibility.Collapsed;
            }

        }
        private bool CanMyProfileExecute()
        {
            return true;
        }

        private ICommand visiblePost;
        public ICommand VisiblePost
        {
            get
            {
                if (visiblePost == null)
                {
                    visiblePost = new RelayCommand(param => VisiblePostExecute(), param => CanVisiblePostExecute());
                }
                return visiblePost;
            }
        }

        //Return to the login page
        private void VisiblePostExecute()
        {
            Post = new tblPost();
            Service s = new Service();
            Post.PostText = mainWindow.txtPost.Text.ToString();
            Post.UserPost = LoggedGuest.ID;
            DateTime now = DateTime.Now;
            Post.DateTimePost = now;
            post.VisiblePost = 1;

            if (s.AddPost(Post) != null)
            {
                mainWindow.txtPost.Text = "";
                PostPost();
            }
            else
            {
                return;
            }

        }
        private bool CanVisiblePostExecute()
        {
            if (mainWindow.txtPost.Text == "")
            {
                return false;
            }
            else
            {
                return true;

            }
        }
    }
}
