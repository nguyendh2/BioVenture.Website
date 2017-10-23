using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCell.Models
{
    public class ActionChoiceListViewModel
    {
        public List<ActionChoice> ActionChoices { get; set; }
        public string ScenarioName { get; set; }
        public int ScenarioId { get; set; }
    }
}