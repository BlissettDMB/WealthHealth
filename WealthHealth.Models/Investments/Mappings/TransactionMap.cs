using System.Data.Entity.ModelConfiguration;

namespace WealthHealth.Models.Investments.Mappings
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            ToTable("Transactions");
            Property(s => s.UnitPrice).HasPrecision(18, 6);
            Property(s => s.Commission).HasPrecision(18, 6);
            Property(s => s.ShareQuantity).HasPrecision(18, 6);
        }
    }
}