using System.Data.Entity.ModelConfiguration;

namespace WealthHealth.Models.Investments.Mappings
{
    public class BrokerageMap : EntityTypeConfiguration<BrokerageAccount>
    {
        public BrokerageMap()
        {
            HasRequired(a => a.Brokerage);
        }
    }
}