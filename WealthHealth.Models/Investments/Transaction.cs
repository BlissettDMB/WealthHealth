using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WealthHealth.Models.Investments
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Security")]
        public virtual int SecurityId { get; set; }
        public virtual Security Security { get; set; }

        [Required]
        [DisplayName("Brokerage Account")]
        public virtual int BrokerageAccountId { get; set; }
        [DisplayName("Brokerage Account")]
        public virtual BrokerageAccount BrokerageAccount { get; set; }

        [Required]
        [DisplayName("Transaction Date")]
        [DataType(DataType.DateTime)]
        public virtual DateTime TransactionDate { get; set; }

        [Required]
        [DisplayName("Share Quantity")]
        public virtual decimal ShareQuantity { get; set; }

        [Required]
        [DisplayName("Unit Price")]
        public virtual decimal UnitPrice { get; set; }

        [Required]
        public virtual decimal Commission { get; set; }
    }
}