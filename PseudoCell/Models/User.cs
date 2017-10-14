using System.ComponentModel.DataAnnotations;

namespace PseudoCell.Models
{
        public class User
        {
            [Required]
            public int UserId { get; set; }
            [Required]
            [MaxLength(450)]
            public string AspNetUserId { get; set; }
            [Required]
            [MaxLength(30)]
            public string username { get; set; }
            public bool IsManager { get; set; }//can edit/add/delete scenarios and games
            public bool IsStudent { get; set; }
            public bool IsAdmin { get; set; }
            [MaxLength(30)]
            public string StudentId { get; set; }

        }
}
