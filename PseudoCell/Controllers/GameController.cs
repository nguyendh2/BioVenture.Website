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
        public ActionResult Edit(Game model)
        {
            
            
            using (var context = new MyDataContext())
            {
                var retrievedGame = context.Games.FirstOrDefault(x=>x.Id == model.Id);
                retrievedGame.LastChangedBy = User.Identity.Name;
                retrievedGame.LastChangedDate = DateTime.Now;
                retrievedGame.Name = model.Name;
                context.Entry(retrievedGame).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("Details", "Game", new { gameId = model.Id});
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Delete(int gameId)
        {
            using (var context = new MyDataContext())
            {
                var retrievedGame = context.Games.FirstOrDefault(x => x.Id == gameId);
                context.Entry(retrievedGame).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return RedirectToAction("Index","Game");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Edit(int gameId)
        {
            using (var context = new MyDataContext())
            {
                var model = context.Games.FirstOrDefault(x => x.Id == gameId);
                if (model != null)
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int gameId)
        {
            using(var context = new MyDataContext())
            {
                var model = context.Games.FirstOrDefault(x=>x.Id == gameId);
                if (model != null)
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index","Game");
            
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game model)
        {
            if(String.IsNullOrEmpty(model.Name)==false)
            {
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                using (var context = new MyDataContext())
                {
                    context.Games.Add(model);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Game");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
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
                        return View(context.Games);
                //}

            }
            return RedirectToAction("Index","Home");
        }
    }
}