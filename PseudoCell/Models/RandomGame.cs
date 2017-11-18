using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCell.Models
{
    public class RandomGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Game { get; set; }
    }
}