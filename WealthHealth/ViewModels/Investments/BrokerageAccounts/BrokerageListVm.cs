using System.ComponentModel.DataAnnotations;

namespace WealthHealth.ViewModels.Investments.BrokerageAccounts
{
    public class BrokerageListVm
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}