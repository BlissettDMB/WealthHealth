using System.ComponentModel.DataAnnotations;

namespace WealthHealth.ViewModels.Investments.Stocks
{
    public class AddStockVm
    {
        [Required]
        public string Symbol { get; set; }

        [Required]
        public string Title { get; set; }
    }
}