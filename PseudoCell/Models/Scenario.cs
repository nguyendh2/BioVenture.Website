using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace PseudoCell.Models
{
    public class Scenario
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int GameId { get; set; }
        public List<ActionChoice> ActionChoices { get; set; }
    }
}