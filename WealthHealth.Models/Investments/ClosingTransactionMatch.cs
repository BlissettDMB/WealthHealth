using System.ComponentModel.DataAnnotations;

namespace WealthHealth.Models.Investments
{
    public class ClosingTransactionMatch
    {
        [Required]
        public virtual decimal MatchingShareCount { get; set; }

        [Required]
        public virtual int SellTransactionId { get; set; }
        public virtual SellTransaction SellTransaction { get; set; }

        [Required]
        public virtual int PurchaseTransactionId { get; set; }
        public virtual PurchaseTransaction PurchaseTransaction { get; set; }

        [Required]
        public virtual decimal TotalNetProfit { get; set; }
    }
}