using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WealthHealth.Models.Investments
{
    public class PurchaseTransaction : Transaction
    {
        [DisplayName("Matching Sell Transactions")]
        public virtual List<ClosingTransactionMatch> ClosingTransactionMatches { get; set; }

        [Required]
        [DisplayName("Closed Shares")]
        public virtual decimal ClosedShares { get; set; }

        [Required]
        [DisplayName("Remaining Shares")]
        public virtual decimal RemainingShares { get; set; }

        [Required]
        [DisplayName("Position Closed")]
        public virtual bool PositionClosedStatus { get; set; }
    }
}