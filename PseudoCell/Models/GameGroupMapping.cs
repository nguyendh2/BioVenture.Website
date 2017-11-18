using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PseudoCell.Models
{
    public class GameGroupMapping
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}