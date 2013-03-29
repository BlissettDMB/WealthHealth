using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WealthHealth.Models.Investments
{
    public class Brokerage
    {
        [Required]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Title { get; set; }

        public virtual ICollection<BrokerageAccount> BrokerageAccounts { get; set; }
    }
}