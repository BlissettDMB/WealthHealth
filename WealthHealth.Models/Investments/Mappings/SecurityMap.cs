using System.Data.Entity.ModelConfiguration;

namespace WealthHealth.Models.Investments.Mappings
{
    public class SecurityMap : EntityTypeConfiguration<Security>
    {
        public SecurityMap()
        {
            ToTable("Securities");
        }
    }
}