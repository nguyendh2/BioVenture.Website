using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PseudoCell.DataAccess;
using PseudoCell.Models;

namespace PseudoCell.Controllers
{
    [Authorize]
    public class ScenarioController:Controller
    {
        private MyUserManager _myUserManager;

        public ScenarioController()
        {
            
        }

        public ScenarioController(MyUserManager myUserManager)
        {
            _myUserManager = myUserManager;
        }

        private ActionResult RedirectToHomeIfNotAdmin()
        {
            if (MyUserManager.IsManager(User.Identity.GetUserId()) == false)
                return RedirectToAction("Index", "Home");

            return null;
        }

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

        [Authorize]
        public ActionResult Index(int gameId)
        {
            List<Scenario> scenarios;
            Game game;
            var gameName = "[Game Name Here]";
            using (var context = new MyDataContext())
            {
                scenarios = context.Scenarios.Where(x => x.GameId == gameId).ToList();
                game = context.Games.FirstOrDefault(x => x.Id == gameId);
                if (game != null) gameName = game.Name;
                
            }
            var model = new ScenarioListViewModel() { Scenarios = scenarios, GameName = gameName, GameId = gameId};
            model.IsManager = MyUserManager.IsManager(User.Identity.GetUserId());
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int gameId)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            var model = new Scenario() {GameId = gameId};
            using (var context = new MyDataContext())
            {
                var game = context.Games.FirstOrDefault(x => x.Id == gameId);
                var gameName = game.Name;
                model.GameName = gameName;
            }
            return View(model);
        }

        public ActionResult BackToScenarioList()
        {
            return RedirectToAction("Index", "Game");
        }

        [HttpGet]
        public ActionResult GoToActionChoice(int scenarioId)
        {
            return RedirectToAction("Index", "ActionChoice", new {scenarioId});
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Scenario model)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            model.CreatedBy = User.Identity.Name;
            model.CreatedDate = DateTime.Now;
            using (var context = new MyDataContext())
            {
                context.Scenarios.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { gameId = model.GameId });
        }

        [HttpGet]
        public ActionResult Delete(int scenarioId)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            int gameId;
            using (var context = new MyDataContext())
            {
                var retrievedScenario = context.Scenarios.FirstOrDefault(x => x.Id == scenarioId);
                gameId = retrievedScenario.GameId;
                context.Entry(retrievedScenario).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Scenario", new{ gameId });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int scenarioId)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            var model = new Scenario();
            using (var context = new MyDataContext())
            {
                model = context.Scenarios.FirstOrDefault(x => x.Id == scenarioId);
                var gameId = model.GameId;
                var game = context.Games.FirstOrDefault(x => x.Id == gameId);
                var gameName = game.Name;
                model.GameName = gameName;
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int scenarioId)
        {
            var model = new Scenario();
            using (var context = new MyDataContext())
            {
                model = context.Scenarios.FirstOrDefault(x => x.Id == scenarioId);
            }
            return View(model);
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Scenario model)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            using (var context = new MyDataContext())
            {
                var retrievedScenario = context.Scenarios.FirstOrDefault(x => x.Id == model.Id);
                retrievedScenario.LastChangedBy = User.Identity.Name;
                retrievedScenario.LastChangedDate = DateTime.Now;
                retrievedScenario.Name = model.Name;
                retrievedScenario.Description = model.Description;
                context.Entry(retrievedScenario).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("Details", "Scenario", new {scenarioId = model.Id});
        }
    }

    
}