using System.ComponentModel.DataAnnotations;

namespace WealthHealth.ViewModels.Investments.MutualFunds
{
    public class EditMutualFundVm
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public string Title { get; set; }
    }
}