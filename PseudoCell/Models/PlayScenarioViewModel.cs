using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCell.Models
{
    public class PlayScenarioViewModel
    {
        public Scenario Scenario { get; set; }
        public List<ActionChoice> ActionChoices { get; set; }
    }
}