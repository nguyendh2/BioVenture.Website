using System;
using System.ComponentModel.DataAnnotations;

namespace PseudoCell.Models
{
    public class GameResult
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ActionChoiceId { get; set; }
        public string Comments { get; set; }
        [Required]
        public string AspNetUserId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string GradeLetter { get; set; }
        public double? GradePercent { get; set; }
        [Required]
        public DateTime CompleteDate { get; set; }
    }
}