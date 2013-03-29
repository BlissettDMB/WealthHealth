using System.Data.Entity.ModelConfiguration;

namespace WealthHealth.Models.Investments.Mappings
{
    public class ClosingTransactionMatchMap : EntityTypeConfiguration<ClosingTransactionMatch>
    {
        public ClosingTransactionMatchMap()
        {
            ToTable("Transactions_Match");

            // Closing Transaction Match has a composite key linking the purchase and sell transactions
            HasKey(c => new { c.PurchaseTransactionId, c.SellTransactionId });

            HasRequired(c => c.SellTransaction)
                .WithMany(s => s.ClosingTransactionMatches)
                .HasForeignKey(c => c.SellTransactionId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.PurchaseTransaction)
                .WithMany(s => s.ClosingTransactionMatches)
                .HasForeignKey(c => c.PurchaseTransactionId)
                .WillCascadeOnDelete(false);

            Property(s => s.MatchingShareCount).HasPrecision(18, 6);
            Property(s => s.TotalNetProfit).HasPrecision(18, 2);
        }
    }
}