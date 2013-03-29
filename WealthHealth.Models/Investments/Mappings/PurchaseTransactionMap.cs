using System.Data.Entity.ModelConfiguration;

namespace WealthHealth.Models.Investments.Mappings
{
    public class PurchaseTransactionMap : EntityTypeConfiguration<PurchaseTransaction>
    {
        public PurchaseTransactionMap()
        {
            ToTable("Transactions_Purchase");
            Property(s => s.ClosedShares).HasPrecision(18, 6);
            Property(s => s.RemainingShares).HasPrecision(18, 6);
        }
    }
}