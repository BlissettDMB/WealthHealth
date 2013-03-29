using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WealthHealth.Models.Investments
{
    public class SellTransaction : Transaction
    {
        [DisplayName("Matching Purchase Transactions")]
        public virtual List<ClosingTransactionMatch> ClosingTransactionMatches { get; set; }

        [Required]
        public virtual decimal TotalNetProfit { get; set; }

        [Required]
        public virtual bool Profitable { get; set; }

        [Required]
        public virtual decimal LongTermPositionsClosed { get; set; }

        [Required]
        public virtual decimal LongTermPositionsNetProfit { get; set; }

        [Required]
        public virtual decimal ShortTermPositionsClosed { get; set; }

        [Required]
        public virtual decimal ShortTermPositionsNetProfit { get; set; }
    }
}