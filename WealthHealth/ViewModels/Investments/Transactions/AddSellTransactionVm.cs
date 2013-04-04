using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WealthHealth.ViewModels.Investments.Transactions
{
    public class AddSellTransactionVm
    {
        [Required]
        [DisplayName("Security")]
        public int SecurityId { get; set; }

        [Required]
        [DisplayName("Brokerage Account")]
        public int BrokerageAccountId { get; set; }

        [Required]
        [DisplayName("Transaction Date")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        [Required]
        [DisplayName("Share Quantity")]
        public decimal ShareQuantity { get; set; }

        [Required]
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal Commission { get; set; }

        [Required]
        public List<TransactionMatchVm> MatchingPurchaseTransactions { get; set; }
    }
}