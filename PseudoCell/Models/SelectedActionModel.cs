using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCell.Models
{
    public class SelectedActionModel
    {
        public int actionChoiceId { get; set; }
        public Guid PageGuid { get; set; }
    }
}