using System.Data.Entity;

namespace WealthHealth.DbManagement
{
    public class BaseContext<TContext> : DbContext where TContext : DbContext
    {
        static BaseContext()
        {
            System.Data.Entity.Database.SetInitializer<TContext>(null);
        }

        protected BaseContext()
            : base("WealthHealthDB")
        {

        }
    }
}
