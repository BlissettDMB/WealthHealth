using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WealthHealth.ViewModels.Investments.BrokerageAccounts
{
    public class AddBrokerageAccountVm
    {
        [Required]
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public bool IsRetirement { get; set; }

        [Required]
        public int BrokerageId { get; set; }
    }
}