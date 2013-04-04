using System.Collections.Generic;
using WealthHealth.Models.Investments;

namespace WealthHealth.ViewModels.Investments.Transactions
{
    public class SecurityPurchaseHistoryVm
    {
        public Security Security { get; set; }
        public decimal TotalOpenShareQuantity { get; set; }
        public decimal DollarCost { get; set; }
        public ICollection<PurchaseTransaction> PurchaseTransactions { get; set; }
    }
}