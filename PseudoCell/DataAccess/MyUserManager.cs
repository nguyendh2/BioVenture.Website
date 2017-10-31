using System;
using System.Linq;
using PseudoCell.Models;

namespace PseudoCell.DataAccess
{
    public class MyUserManager :IDisposable
    {
        private MyDataContext myDataContext;
        public MyUserManager()
        {
            myDataContext = new MyDataContext();
        }

        public bool IsManager(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return false;
            var result = myDataContext.Users.FirstOrDefault(x=>x.AspNetUserId.Equals(id));
            return result.IsManager;
        }

        public bool IsAdmin(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return false;
            var result = myDataContext.Users.FirstOrDefault(x => x.AspNetUserId.Equals(id));
            return result.IsAdmin;
        }

        public bool IsStudent(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return false;
            var result = myDataContext.Users.FirstOrDefault(x => x.AspNetUserId.Equals(id));
            return result.IsStudent;
        }

        public bool AddUser(User user)
        {
            if (user != null)
            {
                var result = myDataContext.Users.Add(user);
                myDataContext.SaveChanges();
                return result != null;
            }
            else
            {
                return false;
            }
        }

        public static MyUserManager Create()
        {
            return new MyUserManager();
        }

        public void Dispose()
        {
            myDataContext = null;
        }
    }
}