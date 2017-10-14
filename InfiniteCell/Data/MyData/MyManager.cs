//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace InfiniteCell.Data.MyData
//{
//    public class MyManager
//    {
//        private MyDataContext myDataContext;
//        public MyManager()
//        {
//            myDataContext = new MyDataContext();
//        }

//        public User GetUserByAspNetUserId(string id)
//        {
//            if (String.IsNullOrWhiteSpace(id)) return null;
//            var result = myDataContext.Users.Find(new { AspNetUserId = id });
//            return result;
//        }

//        public bool AddUser(User user)
//        {
//            if (user != null)
//            {
//                var result = myDataContext.Users.Add(user);
//                return result != null;
//            }
//            else
//            {
//                return false;
//            }
//        }
//    }
//}
