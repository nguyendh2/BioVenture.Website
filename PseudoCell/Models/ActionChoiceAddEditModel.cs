using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PseudoCell.Models
{
    public class ActionChoiceAddEditModel
    {
        public ActionChoice ActionChoice { get; set; }
        public List<SelectListItem> ScenariosForSelection { get; set; }
        public string ScenarioName { get; set; }
    }
}