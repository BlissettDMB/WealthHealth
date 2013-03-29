using System.Data.Entity;
using WealthHealth.Models.Core;
using WealthHealth.Models.Investments;
using WealthHealth.Models.Investments.Mappings;

namespace WealthHealth.DbManagement
{
    public class WealthHealthDB : BaseContext<WealthHealthDB>
    {
        public DbSet<User> Users { get; set; }

        // Investments
        public DbSet<Brokerage> Brokerages { get; set; }
        public DbSet<BrokerageAccount> BrokerageAccounts { get; set; }

        // Securities
        public DbSet<Security> Securities { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<MutualFund> MutualFunds { get; set; }

        // Transactions
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PurchaseTransaction> PurchaseTransactions { get; set; }
        public DbSet<SellTransaction> SellTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Investments
            modelBuilder.Configurations.Add(new BrokerageMap());

            // Securities
            modelBuilder.Configurations.Add(new SecurityMap());
            modelBuilder.Configurations.Add(new StockMap());
            modelBuilder.Configurations.Add(new MutualFundMap());

            // Transactions
            modelBuilder.Configurations.Add(new TransactionMap());
            modelBuilder.Configurations.Add(new PurchaseTransactionMap());
            modelBuilder.Configurations.Add(new SellTransactionMap());
            modelBuilder.Configurations.Add(new ClosingTransactionMatchMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}