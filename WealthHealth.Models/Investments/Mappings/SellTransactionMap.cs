using System.Data.Entity.ModelConfiguration;

namespace WealthHealth.Models.Investments.Mappings
{
    public class SellTransactionMap : EntityTypeConfiguration<SellTransaction>
    {
        public SellTransactionMap()
        {
            ToTable("Transactions_Sell");
            Property(s => s.TotalNetProfit).HasPrecision(18, 2);
            Property(s => s.LongTermPositionsClosed).HasPrecision(18, 6);
            Property(s => s.LongTermPositionsNetProfit).HasPrecision(18, 2);
            Property(s => s.ShortTermPositionsClosed).HasPrecision(18, 6);
            Property(s => s.ShortTermPositionsNetProfit).HasPrecision(18, 2);
        }
    }
}