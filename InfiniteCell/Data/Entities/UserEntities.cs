using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfiniteCell.Data.Entities
{
    public class UserEntities
    {
        public int AspNetUserId { get; set; }
        public string username { get; set; }
        public bool IsManager { get; set; }//can edit/add/delete scenarios and games
        public bool IsStudent { get; set; }
        public bool IsAdmin { get; set; }

    }
}
