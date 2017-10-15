using System.ComponentModel.DataAnnotations;

namespace PseudoCell.Models
{
    public class ActionChoice
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int NextScenarioId { get; set; }
    }
}

//Game -> 1 initial Scenario -> Many Action Choices
//Each action choice points to another scenario
