using System.ComponentModel.DataAnnotations;

namespace WealthHealth.ViewModels.Investments.MutualFunds
{
    public class AddMutualFundVm
    {
        [Required]
        public string Symbol { get; set; }

        [Required]
        public string Title { get; set; }
    }
}