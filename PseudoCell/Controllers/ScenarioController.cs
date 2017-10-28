using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using PseudoCell.DataAccess;
using PseudoCell.Models;

namespace PseudoCell.Controllers
{
    [Authorize]
    public class ScenarioController:Controller
    {
        public ScenarioController()
        {
            
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
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int gameId)
        {
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