using System;
using PseudoCell.Models;

namespace PseudoCell.DataAccess
{
    public class MyUserManager
    {
        private MyDataContext myDataContext;
        public MyUserManager()
        {
            myDataContext = new MyDataContext();
        }

        public User GetUserByAspNetUserId(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return null;
            var result = myDataContext.Users.Find(new { AspNetUserId = id });
            return result;
        }

        public bool AddUser(User user)
        {
            if (user != null)
            {
                var result = myDataContext.Users.Add(user);
                return result != null;
            }
            else
            {
                return false;
            }
        }
    }
}