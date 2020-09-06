using SocialMedia.Command;
using SocialMedia.Models;
using SocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

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
            if(PostList.Count > 0)
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

                FriendList = s.GetAllUser();
                if(friendList.Count == 1)
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
