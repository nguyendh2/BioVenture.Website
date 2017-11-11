using System.Data.Entity;
using PseudoCell.Models;

namespace PseudoCell.DataAccess
{
    public class MyDataContext : DbContext
    {
        public MyDataContext() : base("DefaultConnection")
        {
            
        }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<ActionChoice> ActionChoices { get; set; }
        public DbSet<GameResult> GameResults { get; set; }

        public System.Data.Entity.DbSet<PseudoCell.Models.GameResultViewEditModel> GameResultViewModels { get; set; }

        //public System.Data.Entity.DbSet<PseudoCell.Models.GameResultViewModel> GameResultViewModels { get; set; }
    }
}