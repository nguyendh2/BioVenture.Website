using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCell.Models
{
    public class ScenarioListViewModel
    {
        public List<Scenario> Scenarios { get; set; }
        public string GameName { get; set; }
        public int GameId { get; set; }
        public bool IsManager { get; set; }
    }
}