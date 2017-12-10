using System;
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
        public string GameName { get; set; }
        public List<ActionChoice> ActionChoices { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCommentRequired { get; set; }
    }
}