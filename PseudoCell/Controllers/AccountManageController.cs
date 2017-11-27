using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using PseudoCell.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using PseudoCell.Models;


namespace PseudoCell.Controllers
{
    public class AccountManageController : Controller
    {
        private MyUserManager _myUserManager;
        public MyUserManager MyUserManager
        {
            get
            {
                return _myUserManager ?? HttpContext.GetOwinContext().Get<MyUserManager>();
            }
            private set
            {
                _myUserManager = value;
            }
        }
        public AccountManageController()
        {

        }

        public AccountManageController(MyUserManager myUserManager)
        {
            _myUserManager = myUserManager;
        }

        [Authorize]
        public ActionResult Index()
        {
            if (MyUserManager.IsAdmin(User.Identity.GetUserId()))
            {
                var model = new List<User>();
                using (var context = new MyDataContext())
                {
                    model = context.Users.Where(x => x.IsAdmin == false).ToList();
                }
                return View(model);
            }

            return RedirectToAction("Details", new { id =User.Identity.GetUserId()});
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = new PseudoCell.Models.User();
            using (var context = new MyDataContext())
            {
                model = context.Users.FirstOrDefault(x=>x.AspNetUserId.Equals(id,StringComparison.OrdinalIgnoreCase));
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = new PseudoCell.Models.User();
            using (var context = new MyDataContext())
            {
                model = context.Users.FirstOrDefault(x => x.AspNetUserId.Equals(id, StringComparison.OrdinalIgnoreCase));
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(PseudoCell.Models.User model)
        {
            using (var context = new MyDataContext())
            {
                var retrievedUser = context.Users.FirstOrDefault(x => x.AspNetUserId.Equals(model.AspNetUserId, StringComparison.OrdinalIgnoreCase));
                retrievedUser.IsManager = model.IsManager;
                retrievedUser.IsStudent = model.IsStudent;
                retrievedUser.StudentId = model.StudentId;
                context.Entry(retrievedUser).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("Details",new { id=model.AspNetUserId});
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(string id)
        {
            using (var context = new MyDataContext())
            {
                var retrievedUser = context.Users.FirstOrDefault(x => x.AspNetUserId.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (retrievedUser != null) {
                    context.Entry(retrievedUser).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}