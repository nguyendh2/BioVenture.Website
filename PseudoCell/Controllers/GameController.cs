using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using PseudoCell.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web.Caching;
using PseudoCell.Models;

namespace PseudoCell.Controllers
{
    [Authorize]
    public class GameController:Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private MyUserManager _myUserManager;
        private string _previousPageGuid = "previousPageGuid";
        private int _maxGuidCount = 25;

        public GameController()
        {
            
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditGameResult(int id)
        {
            if (MyUserManager.IsManager(User.Identity.GetUserId()) == false)
                return RedirectToAction("Index", "Home");

            var model = new GameResultViewEditModel();
            using (var context = new MyDataContext())
            {
                var gameResult = context.GameResults.FirstOrDefault(x => x.Id == id);
                if (gameResult == null) return RedirectToAction("GameResultIndex");

                var actionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == gameResult.ActionChoiceId);
                var scenario = context.Scenarios.FirstOrDefault(x => x.Id == actionChoice.ScenarioId);
                var game = context.Games.FirstOrDefault(x => x.Id == scenario.GameId);

                model.ActionChoiceName = actionChoice?.Name;
                model.ScenarioName = scenario?.Name;
                model.GameName = game?.Name;

                model.Id = gameResult.Id;
                model.ActionChoiceId = gameResult.ActionChoiceId;
                model.AspNetUserId = gameResult.AspNetUserId;
                model.Comments = gameResult.Comments;
                model.CompleteDate = gameResult.CompleteDate;
                model.GradeLetter = gameResult.GradeLetter;
                model.GradePercent = gameResult.GradePercent;
                model.StudentId = gameResult.StudentId;
                model.StudentName = gameResult.StudentName;
            }
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public ActionResult EditGameResult(GameResultViewEditModel model)
        {
            if (MyUserManager.IsManager(User.Identity.GetUserId()) == false)
                return RedirectToAction("Index", "Home");

            using (var context = new MyDataContext())
            {
                var retrievedGameResult = context.GameResults.FirstOrDefault(x => x.Id == model.Id);
                retrievedGameResult.GradeLetter = model.GradeLetter;
                retrievedGameResult.GradePercent = model.GradePercent;
                retrievedGameResult.LastChangedDateTime = DateTime.Now;
                retrievedGameResult.LastChangedBy = User.Identity.GetUserName();

                context.Entry(retrievedGameResult).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("ViewGameResult",new{id=model.Id});
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteGameResult(int id)
        {
            if (MyUserManager.IsManager(User.Identity.GetUserId()) == false)
                return RedirectToAction("Index", "Home");

            using (var context = new MyDataContext())
            {
                var gameResult = context.GameResults.FirstOrDefault(x => x.Id == id);
                context.Entry(gameResult).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            return RedirectToAction("GameResultIndex");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ViewGameResult(int id)
        {
            if (MyUserManager.IsManager(User.Identity.GetUserId()) == false)
                return RedirectToAction("Index", "Home");

            var model = new GameResultViewEditModel();
            using (var context = new MyDataContext())
            {
                var gameResult = context.GameResults.FirstOrDefault(x => x.Id == id);
                if (gameResult == null) return RedirectToAction("GameResultIndex");

                var actionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == gameResult.ActionChoiceId);
                var scenario = context.Scenarios.FirstOrDefault(x => x.Id == actionChoice.ScenarioId);
                var game = context.Games.FirstOrDefault(x => x.Id == scenario.GameId);

                model.ActionChoiceName = actionChoice?.Name;
                model.ScenarioName = scenario?.Name;
                model.GameName = game?.Name;

                model.Id = gameResult.Id;
                model.ActionChoiceId = gameResult.ActionChoiceId;
                model.AspNetUserId = gameResult.AspNetUserId;
                model.Comments = gameResult.Comments;
                model.CompleteDate = gameResult.CompleteDate;
                model.GradeLetter = gameResult.GradeLetter;
                model.GradePercent = gameResult.GradePercent;
                model.StudentId = gameResult.StudentId;
                model.StudentName = gameResult.StudentName;
            }
            return View(model);
        }

        public bool VerifySessionToken(Guid currentPageGuid)//verify and delete token
        {
            var currentUserId = User.Identity.GetUserId();
            var result = false;
            using (var context = new MyDataContext())
            {
                var retrievedPageToken = context.PageGuids.FirstOrDefault(x => x.PageGuid.CompareTo(currentPageGuid)==0);
                if (retrievedPageToken != null && retrievedPageToken.AspNetUserId.Equals(currentUserId))
                {
                    result = true;
                    context.Entry(retrievedPageToken).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }

            }
            return result;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Select(SelectedActionModel selectedActionModel)
        {
            var actionChoiceId = selectedActionModel.actionChoiceId;
            var pageGuid = selectedActionModel.PageGuid;
            
            //Check if player abused the browser's back button
            if (VerifySessionToken(pageGuid)==false)
            {
                return RedirectToAction("Index");
            }

            var model = new PlayScenarioViewModel();
            using (var context = new MyDataContext())
            {
                var actionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == actionChoiceId);
                var nextScenario = context.Scenarios.FirstOrDefault(x => x.Id == actionChoice.NextScenarioId);
                if (nextScenario == null)
                {
                    return RedirectToAction("EndGame",new{actionChoiceId});
                }
                var actionChoices = context.ActionChoices.Where(x => x.ScenarioId == nextScenario.Id).ToList();
                var game = context.Games.FirstOrDefault(x => x.Id == nextScenario.GameId);
                model.Scenario = nextScenario;
                model.ActionChoices = actionChoices;
                if(game!=null) model.Scenario.GameName = game.Name;

            }

            model.PageGuid = Guid.NewGuid();
            SavePageGuid(model.PageGuid);

            return View("Play",model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EndGame(int actionChoiceId)
        {
            var model = new GameResultViewEditModel() {ActionChoiceId = actionChoiceId};
            using (var context = new MyDataContext())
            {
                var actionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == actionChoiceId);
                var scenario = context.Scenarios.FirstOrDefault(x => x.Id == actionChoice.ScenarioId);
                var game = context.Games.FirstOrDefault(x => x.Id == scenario.GameId);

                model.ActionChoiceName = actionChoice?.Name;
                model.ScenarioName = scenario?.Name;
                model.GameName = game?.Name;
            }
            return View(model);
        }

        [Authorize]
        public ActionResult GameResultIndexStudent()
        {
            var model = new List<GameResultViewEditModel>();
            using (var context = new MyDataContext())
            {
                var userId = User.Identity.GetUserId();
                var gameResults = context.GameResults.Where(x => x.AspNetUserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
                foreach (var result in gameResults)
                {
                    var newGameResult = new GameResultViewEditModel();

                    var actionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == result.ActionChoiceId);

                    if (actionChoice != null)
                    {
                        var scenario = context.Scenarios.FirstOrDefault(x => x.Id == actionChoice.ScenarioId);
                        var game = context.Games.FirstOrDefault(x => x.Id == scenario.GameId);

                        newGameResult.ActionChoiceName = actionChoice?.Name;
                        newGameResult.ScenarioName = scenario?.Name;
                        newGameResult.GameName = game?.Name;

                    }
                    else
                    {
                        newGameResult.ActionChoiceName = "N/A - ActionChoice Not Found";
                        newGameResult.ScenarioName = "N/A - ActionChoice Not Found";
                        newGameResult.GameName = "N/A - ActionChoice Not Found";
                    }

                    newGameResult.Comments = result.Comments;
                    newGameResult.Id = result.Id;
                    newGameResult.ActionChoiceId = result.ActionChoiceId;
                    newGameResult.StudentName = result.StudentName;
                    newGameResult.GradeLetter = result.GradeLetter;
                    newGameResult.GradePercent = result.GradePercent;
                    newGameResult.CompleteDate = result.CompleteDate;

                    model.Add(newGameResult);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GameResultIndex()
        {
            if (MyUserManager.IsManager(User.Identity.GetUserId()) == false)
                return RedirectToAction("Index", "Home");

            var model = new List<GameResultViewEditModel>();
            using (var context = new MyDataContext())
            {
                foreach (var result in context.GameResults)
                {
                    var newGameResult = new GameResultViewEditModel();
                    
                    var actionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == result.ActionChoiceId);

                    if (actionChoice != null)
                    {
                        var scenario = context.Scenarios.FirstOrDefault(x => x.Id == actionChoice.ScenarioId);
                        var game = context.Games.FirstOrDefault(x => x.Id == scenario.GameId);

                        newGameResult.ActionChoiceName = actionChoice?.Name;
                        newGameResult.ScenarioName = scenario?.Name;
                        newGameResult.GameName = game?.Name;
                        
                    }
                    else
                    {
                        newGameResult.ActionChoiceName = "N/A - ActionChoice Not Found";
                        newGameResult.ScenarioName = "N/A - ActionChoice Not Found";
                        newGameResult.GameName = "N/A - ActionChoice Not Found";
                    }

                    newGameResult.Comments = result.Comments;
                    newGameResult.Id = result.Id;
                    newGameResult.ActionChoiceId = result.ActionChoiceId;
                    newGameResult.StudentName = result.StudentName;
                    newGameResult.GradeLetter = result.GradeLetter;
                    newGameResult.GradePercent = result.GradePercent;
                    newGameResult.CompleteDate = result.CompleteDate;

                    model.Add(newGameResult);
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EndGame(GameResultViewEditModel gameResultViewEditModel)
        {
            var gameResultToSave = new GameResult()
            {
                ActionChoiceId = gameResultViewEditModel.ActionChoiceId,
                Comments = gameResultViewEditModel.Comments,
                AspNetUserId = User.Identity.GetUserId(),
                StudentName = User.Identity.GetUserName(),
                CompleteDate = DateTime.Now,
                LastChangedDateTime = DateTime.Now,
                LastChangedBy = User.Identity.GetUserName()
            };

            using (var context = new MyDataContext())
            {
                context.GameResults.Add(gameResultToSave);
                context.SaveChanges();
            }
            
            return RedirectToAction("GameResultDetails",new{gameResultId = gameResultToSave.Id});
        }

        [HttpGet]
        [Authorize]
        public ActionResult GameResultDetails(int gameResultId)
        {
            var model = new GameResultViewEditModel();
            using (var context = new MyDataContext())
            {
                var gameResult = context.GameResults.FirstOrDefault(x => x.Id == gameResultId);
                
                var actionChoice = context.ActionChoices.FirstOrDefault(x => x.Id == gameResult.ActionChoiceId);
                var scenario = context.Scenarios.FirstOrDefault(x => x.Id == actionChoice.ScenarioId);
                var game = context.Games.FirstOrDefault(x => x.Id == scenario.GameId);

                model.ActionChoiceName = actionChoice?.Name;
                model.ScenarioName = scenario?.Name;
                model.GameName = game?.Name;
                
                model.Id = gameResult.Id;
                model.ActionChoiceId = gameResult.Id;
                model.AspNetUserId = gameResult.AspNetUserId;
                model.GradeLetter = gameResult.GradeLetter;
                model.GradePercent = gameResult.GradePercent;
                model.Comments = gameResult.Comments;
                model.StudentName = gameResult.StudentName;
                model.StudentId = gameResult.StudentId;


            }
            return View(model);
        }

        public GameController(MyUserManager myUserManager)
        {
            _myUserManager = myUserManager;
        }

        private ActionResult RedirectToHomeIfNotAdmin()
        {
            if (MyUserManager.IsManager(User.Identity.GetUserId()) == false)
                return RedirectToAction("Index","Home");

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

        public void SavePageGuid(Guid guid)
        {
            var userId = User.Identity.GetUserId();
            var model = new PageToken()
            {
                AspNetUserId = userId,
                PageGuid = guid
            };
            using(var context = new MyDataContext())
            {
                context.PageGuids.Add(model);
                context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult Play(int gameId)
        {
            var scenario = new Scenario();
            var model = new PlayScenarioViewModel();
            List<ActionChoice> actionChoices;
            using (var context = new MyDataContext())
            {
                var game = context.Games.FirstOrDefault(x => x.Id == gameId);
                scenario = context.Scenarios.FirstOrDefault(x => x.Id == game.FirstScenarioId);
                if(game!=null) scenario.GameName = game.Name;
                actionChoices = context.ActionChoices.Where(x=>x.ScenarioId == game.FirstScenarioId).ToList();
            }

            model.PageGuid = Guid.NewGuid();
            SavePageGuid(model.PageGuid);

            model.Scenario = scenario;
            model.ActionChoices = actionChoices;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Game model)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

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
        [Authorize]
        public ActionResult Delete(int gameId)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            using (var context = new MyDataContext())
            {
                var retrievedGame = context.Games.FirstOrDefault(x => x.Id == gameId);
                context.Entry(retrievedGame).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return RedirectToAction("Index","Game");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int gameId)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

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
        [Authorize]
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game model)
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

            if (String.IsNullOrEmpty(model.Name)==false)
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

        [Authorize]
        [HttpGet]
        public ActionResult Create ()
        {
            var returnView = RedirectToHomeIfNotAdmin();
            if (returnView != null) return returnView;

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

        [Authorize]
        public ActionResult GoToScenarios(int gameId)
        {
            return RedirectToAction("Index", "Scenario", new {gameId});
        }

        [Authorize]
        public ActionResult Index()
        {
            var model = new GameListViewModel();
            using (var context = new MyDataContext())
            {
                model.Games = context.Games.ToList();
                model.IsManager = MyUserManager.IsManager(User.Identity.GetUserId());
            }
            return View(model);
        }
    }
}