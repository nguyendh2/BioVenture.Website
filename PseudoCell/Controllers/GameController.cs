using System;
using System.Web;
using System.Web.Mvc;
using PseudoCell.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using PseudoCell.Models;
using System.Threading.Tasks;

namespace PseudoCell.Controllers
{
    public class GameController:Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public GameController()
        {
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameModel model)
        {
            if(String.IsNullOrEmpty(model.GameName)==false)
            {
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                using (var context = new MyDataContext())
                {
                    context.GameModels.Add(model);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Game");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create ()
        {
            return View ();
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