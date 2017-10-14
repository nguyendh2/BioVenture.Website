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
    }
}