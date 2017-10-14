using System;
using System.Web;
using System.Web.Mvc;
using PseudoCell.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;

namespace PseudoCell.Controllers
{
    public class GameController:Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public GameController()
        {
        }

        public GameController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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
        public ActionResult Index()
        {
            var context = new MyDataContext();
            if (User.Identity.IsAuthenticated)
            {
                //var user = UserManager.FindById(User.Identity.GetUserId());
                //using (var userContext = new MyDataContext())
                //{
                //    var result = userContext.Users.FirstOrDefault(x => x.AspNetUserId.Equals(user.Id, StringComparison.OrdinalIgnoreCase));
                //    if(result.IsManager || result.IsAdmin)
                        return View(context.GameModels);
                //}

            }
            return RedirectToAction("Index","Home");
        }
    }
}