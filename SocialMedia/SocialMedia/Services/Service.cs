using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace SocialMedia.Services
{
    class Service
    {

        public List<tblUser> GetAllUser()
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from e in context.tblUsers select e).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblUser GetAllUserID(int ID)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    tblUser user = (from e in context.tblUsers where e.UserID == ID select e).First();
                    
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblUser GetUsernamePassword(string username, string password)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    string usernameFromDB = (from e in context.tblUsers where e.UserUsername == username select e.UserUsername).FirstOrDefault();

                    if (username == usernameFromDB)
                    {
                        tblUser user = (from e in context.tblUsers where e.UserUsername.Equals(username) where e.UserPassword.Equals(password) select e).First();
                        return user;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblUser AddUser(tblUser user)
        {
            try
            {

                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    if (user.UserID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            UserID = user.UserID,
                            UserName = user.UserName,
                            UserSurname = user.UserSurname,
                            UserUsername = user.UserUsername,
                            UserPassword = user.UserPassword,
                            DateOfBirth = user.DateOfBirth,
                            VisibleDate = user.VisibleDate,
                            Gender = user.Gender,
                            VisibleGender = user.VisibleGender
                        };

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;
                        return user;
                    }
                    else
                    {
                        tblUser userToEdit = (from r in context.tblUsers where r.UserID == user.UserID select r).First();

                        userToEdit.UserID = user.UserID;
                        userToEdit.UserName = user.UserName;
                        userToEdit.UserSurname = user.UserSurname;
                        userToEdit.UserUsername = user.UserUsername;
                        userToEdit.UserPassword = user.UserPassword;
                        userToEdit.DateOfBirth = user.DateOfBirth;
                        userToEdit.VisibleDate = user.VisibleDate;
                        userToEdit.Gender = user.Gender;
                        userToEdit.VisibleGender = user.VisibleGender;
                        context.SaveChanges();
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }

        public tblUser GetUserUsername(string username)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    string usernameFromDB = (from e in context.tblUsers where e.UserUsername == username select e.UserUsername).FirstOrDefault();

                    if (username == usernameFromDB)
                    {
                        return (from e in context.tblUsers where e.UserUsername == username select e).FirstOrDefault();
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
