using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PseudoCell.DataAccess;
using PseudoCell.Models;

namespace PseudoCell.Controllers
{
    [Authorize]
    public class ActionChoiceController:Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index(int scenarioId)
        {
            List<ActionChoice> actionChoices;
            Scenario scenario;
            var scenarioName = "[Scenario Name Here]";
            using (var context = new MyDataContext())
            {
                actionChoices = context.ActionChoices.Where(x => x.Id == scenarioId).ToList();
                scenario = context.Scenarios.FirstOrDefault(x => x.Id == scenarioId);
                if (scenario != null) scenarioName = scenario.Name;
            }
            var model = new ActionChoiceListViewModel() { ActionChoices = actionChoices, ScenarioName = scenarioName, ScenarioId = scenarioId };
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int scenarioId)
        {
            var model = new ActionChoice(){ScenarioId = scenarioId};
            using (var context = new MyDataContext())
            {
                var scenario = context.Scenarios.FirstOrDefault(x => x.Id == scenarioId);
                var scenarioname = scenario.Name;
                model.ScenarioName = scenarioname;
            }
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Create(ActionChoice model)
        {
            model.CreatedBy = User.Identity.Name;
            model.CreatedDate = DateTime.Now;
            using (var context = new MyDataContext())
            {
                context.ActionChoices.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { scenarioId = model.ScenarioId });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int actionChoiceId)
        {
            var model = new ActionChoice();
            using (var context = new MyDataContext())
            {
                model = context.ActionChoices.FirstOrDefault(x => x.Id == actionChoiceId);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(ActionChoice model)
        {
            using (var context = new MyDataContext())
            {
                var retrievedActionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == model.Id);
                retrievedActionChoice.LastChangedBy = User.Identity.Name;
                retrievedActionChoice.LastChangedDate = DateTime.Now;
                retrievedActionChoice.Name = model.Name;
                retrievedActionChoice.Description = model.Description;
                context.Entry(retrievedActionChoice).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("Details", "ActionChoice", new { ActionChoiceId = model.Id });
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int actionChoiceId)
        {
            var model = new ActionChoice();
            using (var context = new MyDataContext())
            {
                model = context.ActionChoices.FirstOrDefault(x => x.Id == actionChoiceId);
                var scenarioId = model.ScenarioId;
                var scenario = context.Games.FirstOrDefault(x => x.Id == scenarioId);
                var scenarioname = scenario.Name;
                model.ScenarioName = scenarioname;
            }
            return View(model);
        }



        [HttpGet]
        public ActionResult Delete(int actionChoiceId)
        {
            int scenarioId;
            using (var context = new MyDataContext())
            {
                var retrievedActionChoice = context.Scenarios.FirstOrDefault(x => x.Id == actionChoiceId);
                scenarioId = retrievedActionChoice.GameId;
                context.Entry(retrievedActionChoice).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Scenario", new { gameId = scenarioId });
        }


    }
}