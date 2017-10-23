using System;
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
        public string NextScenarioName { get; set; }
        public int ScenarioId { get; set; }
        public string ScenarioName { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}

//Game -> 1 initial Scenario -> Many Action Choices
//Each action choice points to another scenario
