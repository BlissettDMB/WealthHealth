using System.Data.Entity.ModelConfiguration;

namespace WealthHealth.Models.Investments.Mappings
{
    public class StockMap : EntityTypeConfiguration<Stock>
    {
        public StockMap()
        {
            ToTable("Securities_Stocks");
        }
    }
}