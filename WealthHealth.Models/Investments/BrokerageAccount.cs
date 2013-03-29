using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WealthHealth.Models.Core;

namespace WealthHealth.Models.Investments
{
    public class BrokerageAccount
    {
        public virtual int Id { get; set; }

        [Required]
        [DisplayName("Account Number")]
        public virtual string AccountNumber { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [Required]
        public virtual bool IsRetirement { get; set; }

        [Required]
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public virtual int BrokerageId { get; set; }
        public virtual Brokerage Brokerage { get; set; }
    }
}