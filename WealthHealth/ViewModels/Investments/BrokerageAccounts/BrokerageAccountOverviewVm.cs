using System.Collections.Generic;
using WealthHealth.Models.Investments;

namespace WealthHealth.ViewModels.Investments.BrokerageAccounts
{
    public class BrokerageAccountOverviewVm
    {
        public ICollection<BrokerageAccount> NonRetirementAccounts { get; set; }
        public ICollection<BrokerageAccount> RetirementAccounts { get; set; }
    }
}