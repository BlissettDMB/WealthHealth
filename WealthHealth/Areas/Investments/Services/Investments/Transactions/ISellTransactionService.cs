
using WealthHealth.Models.Investments;
using WealthHealth.ViewModels.Investments.Transactions;

namespace WealthHealth.Areas.Investments.Services.Investments.Transactions
{
    public interface ISellTransactionService
    {
        SellTransaction GetSellTransaction(int sellTransactionId);

        int CreateSellTransaction(AddSellTransactionVm addSellTransactionVm);
    }
}