using System.Data.Entity.ModelConfiguration;

namespace WealthHealth.Models.Investments.Mappings
{
    public class MutualFundMap : EntityTypeConfiguration<MutualFund>
    {
        public MutualFundMap()
        {
            ToTable("Securities_MutualFunds");
        }
    }
}