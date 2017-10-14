using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using PseudoCell.Models;
using PseudoCell.DataAccess;

namespace PseudoCell.Controllers
{
    public class GameController:Controller
    {
        public ActionResult Index()
        {
            var context = new MyDataContext();
            
            return View(context.GameModels);
            
        }
    }
}