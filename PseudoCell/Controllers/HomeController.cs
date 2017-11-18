using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PseudoCell.DataAccess;
using PseudoCell.Models;
using Microsoft.Owin.Security;

namespace PseudoCell.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }
        
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ActionResult Index()
        {
            ViewBag.UserTitle = "Guest";
            if (User.Identity.IsAuthenticated)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                using (var context = new MyDataContext())
                {
                    var result = context.Users.FirstOrDefault(x => x.AspNetUserId.Equals(user.Id,StringComparison.OrdinalIgnoreCase));
                    if (result == null) {
                        ViewBag.UserTitle = "Error - Failed to determine";
                    }
                    else if (result.IsAdmin)
                    {
                        ViewBag.UserTitle = "Admin (Account Status will go away in the next implementation phase)";
                    }
                    else
                    {
                        ViewBag.UserTitle = "Student";
                    }
                }
                    
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }

            base.Dispose(disposing);
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacts";

            return View();
        }
        public ActionResult Help()
        {
            ViewBag.message = "Help";
            return View();
        }
    }
}