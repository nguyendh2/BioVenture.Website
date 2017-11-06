using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PseudoCell.DataAccess;
using PseudoCell.Models;
using Microsoft.AspNet.Identity.Owin;

namespace PseudoCell.Controllers
{
    [Authorize]
    public class ActionChoiceController:Controller
    {
        private MyUserManager _myUserManager;

        public ActionChoiceController()
        {
            
        }

        public ActionChoiceController(MyUserManager myUserManager)
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

        [HttpGet]
        [Authorize]
        public ActionResult Index(int scenarioId)
        {
            List<ActionChoice> actionChoices;
            Scenario scenario;
            var scenarioName = "[Scenario Name Here]";
            using (var context = new MyDataContext())
            {
                actionChoices = context.ActionChoices.Where(x => x.ScenarioId == scenarioId).ToList();
                scenario = context.Scenarios.FirstOrDefault(x => x.Id == scenarioId);
                if (scenario != null) scenarioName = scenario.Name;
            }
            var model = new ActionChoiceListViewModel() { ActionChoices = actionChoices, ScenarioName = scenarioName, ScenarioId = scenarioId };
            model.IsManager = MyUserManager.IsManager(User.Identity.GetUserId());
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int scenarioId)
        {
            var scenariosForSelect = new List<Scenario>();
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            var model = new ActionChoiceAddEditModel(){ActionChoice = new ActionChoice{ScenarioId = scenarioId}};
            using (var context = new MyDataContext())
            {
                var scenario = context.Scenarios.FirstOrDefault(x => x.Id == scenarioId);
                var scenarioname = scenario.Name;
                model.ScenarioName = scenarioname;

                scenariosForSelect = context.Scenarios.Where(x => x.GameId == scenario.GameId && x.Id != scenarioId).ToList();
            }
            
            model.ScenariosForSelection = scenariosForSelect.Select(x=>new SelectListItem
                                                    {
                                                        Value=x.Id.ToString(),
                                                        Text=x.Name
                                                    }).ToList();
            model.ScenariosForSelection.Add(new SelectListItem()
            {
                Value="-1",
                Text= "*End Game*"
            });
                return View(model);
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult Create(ActionChoiceAddEditModel addEditModel)
        {
            var model = addEditModel.ActionChoice;
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            model.CreatedBy = User.Identity.Name;
            model.CreatedDate = DateTime.Now;
            using (var context = new MyDataContext())
            {
                context.ActionChoices.Add(model);
                var nextScenario = context.Scenarios.FirstOrDefault(x => x.Id == model.NextScenarioId);
                if (model.NextScenarioId == -1)
                {
                    model.NextScenarioName = "End Game";
                }
                else
                {
                    model.NextScenarioName = nextScenario.Name;
                }
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
        public ActionResult Edit(ActionChoiceAddEditModel model)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            using (var context = new MyDataContext())
            {
                var retrievedActionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == model.ActionChoice.Id);
                retrievedActionChoice.LastChangedBy = User.Identity.Name;
                retrievedActionChoice.LastChangedDate = DateTime.Now;
                retrievedActionChoice.Name = model.ActionChoice.Name;
                retrievedActionChoice.Description = model.ActionChoice.Description;
                var nextScenario = context.Scenarios.FirstOrDefault(x => x.Id == model.ActionChoice.NextScenarioId);
                retrievedActionChoice.NextScenarioId = nextScenario.Id;
                retrievedActionChoice.NextScenarioName = nextScenario.Name;
                context.Entry(retrievedActionChoice).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("Details", "ActionChoice", new { ActionChoiceId = model.ActionChoice.Id });
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int actionChoiceId)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            var actionChoice = new ActionChoice();
            var scenariosForSelect = new List<Scenario>();
            var model = new ActionChoiceAddEditModel();
            using (var context = new MyDataContext())
            {
                actionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == actionChoiceId);
                var scenarioId = actionChoice.ScenarioId;
                var scenario = context.Scenarios.FirstOrDefault(x => x.Id == scenarioId);
                var scenarioname = scenario.Name;
                actionChoice.ScenarioName = scenarioname;

                scenariosForSelect = context.Scenarios.Where(x => x.GameId == scenario.GameId && x.Id != scenarioId).ToList();
            }
            model.ActionChoice = actionChoice;
            model.ScenariosForSelection = scenariosForSelect.Select(x=>new SelectListItem
                                                                                        {
                                                                                            Value=x.Id.ToString(),
                                                                                            Text=x.Name
                                                                                        }).ToList();

            return View(model);
        }
        
        [HttpGet]
        public ActionResult Delete(int actionChoiceId)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            int scenarioId;
            using (var context = new MyDataContext())
            {
                var retrievedActionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == actionChoiceId);
                scenarioId = retrievedActionChoice.ScenarioId;
                context.Entry(retrievedActionChoice).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "ActionChoice", new { scenarioId });
        }
        
        public ActionResult BackToScenario(int scenarioId)
        {
            return RedirectToAction("Edit", "Scenario", new{scenarioId});
        }

    }
}