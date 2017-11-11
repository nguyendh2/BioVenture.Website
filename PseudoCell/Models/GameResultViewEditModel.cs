using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCell.Models
{
    public class GameResultViewEditModel : GameResult
    {
        public string GameName { get; set; }
        public string ScenarioName { get; set; }
        public string ActionChoiceName { get; set; }
    }
}