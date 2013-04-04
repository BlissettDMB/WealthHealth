using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WealthHealth.Models.Investments;

namespace WealthHealth.ViewModels.Investments.Transactions
{
    public class TransactionMatchVm
    {
        public PurchaseTransaction PurchaseTransaction { get; set; }

        [Required]
        [DisplayName("Purchase Transaction")]
        public int PurchaseTransactionId { get; set; }
        
        [Required]
        [DisplayName("Share Quantity")]
        public decimal ShareQuantity { get; set; }
    }
}