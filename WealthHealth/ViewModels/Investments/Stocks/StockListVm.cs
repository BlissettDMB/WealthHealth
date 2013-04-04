using System.ComponentModel.DataAnnotations;

namespace WealthHealth.ViewModels.Investments.Stocks
{
    public class StockListVm
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}