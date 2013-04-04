
using System.ComponentModel.DataAnnotations;

namespace WealthHealth.ViewModels.Investments.BrokerageAccounts
{
    public class AddBrokerageVm
    {
        [Required]
        public string Title { get; set; }
    }
}