using System;
using System.ComponentModel.DataAnnotations;

namespace PseudoCell.Models
{
    public class Game
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        
        public DateTime? LastChangedDate { get; set; }
        
        public string LastChangedBy { get; set; }
        [Required]
        public int FirstScenarioId { get; set; }
    }
}