using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PseudoCell.Models
{
    public class GameModel
    {
        [Required]
        [Key]
        public int GameId { get; set; }
        [Required]
        public string GameName { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public int FirstScenarioId { get; set; }
    }
}