
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.ApplicationInsights.Web;

namespace PseudoCell.Models
{
    public class PageToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid PageGuid { get; set; }
        [Required]
        public string AspNetUserId { get; set; }
    }
    
}
