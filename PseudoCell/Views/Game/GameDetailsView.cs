using System;
using System.ComponentModel.DataAnnotations;

namespace PseudoCell.Models
{
    public class GameDetailsView { 
        public Game Game { get; set; }
        public bool IsStudent { get; set; }
    }
}