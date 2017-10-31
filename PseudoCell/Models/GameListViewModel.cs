using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCell.Models
{
    public class GameListViewModel
    {
        public List<Game> Games { get; set; }
        public bool IsManager { get; set; }
    }
}