using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PseudoCell.DataAccess;
using PseudoCell.Models;

namespace PseudoCell.Controllers
{
    public class ScenarioController:Controller
    {
        public ScenarioController()
        {
            
        }

        [Authorize]
        public ActionResult Index(int gameId)
        {
            List<Scenario> model;
            using (var context = new MyDataContext())
            {
                model = context.Scenarios.Where(x => x.GameId == gameId).ToList();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int gameId)
        {
            var model = new Scenario() {GameId = gameId};
            
            return View(model);
        }
    }
}