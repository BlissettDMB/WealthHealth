using System.Collections.Generic;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public interface ISellTransactionRepository
    {
        SellTransaction GetSellTransaction(int sellTransactionId);

        ICollection<SellTransaction> GetAllSellTransactions();

        DbOperationStatus InsertSellTransaction(SellTransaction sellTransaction);
        DbOperationStatus UpdateSellTransaction(SellTransaction sellTransaction);
        DbOperationStatus DeleteSellTransaction(SellTransaction sellTransaction);
    }
}