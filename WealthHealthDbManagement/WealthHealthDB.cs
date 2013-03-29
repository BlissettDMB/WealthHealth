using System.Data.Entity;
using WealthHealth.Models.Core;

namespace WealthHealth.DbManagement
{
    public class WealthHealthDB : BaseContext<WealthHealthDB>
    {
        public DbSet<User> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
