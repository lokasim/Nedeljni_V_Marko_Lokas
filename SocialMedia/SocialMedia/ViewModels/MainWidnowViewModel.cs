using SocialMedia.Models;
using SocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MainWidnowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            FriendListChat();
        }

        public void FriendListChat()
        {
            listFr.Add(2);
            //listFr.Add(3);
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
                //for (int i = 0; i < listFr.Count; i++)
                //{
                //        FriendList.Add((tblUser)s.GetAllUserID(listFr[i]));


                //}
                //friendList.Count();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }
    }
}
