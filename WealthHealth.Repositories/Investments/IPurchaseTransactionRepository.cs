using System.Collections.Generic;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public interface IPurchaseTransactionRepository
    {
        PurchaseTransaction GetPurchaseTransaction(int purchaseTransactionId);

        ICollection<PurchaseTransaction> GetAllPurchaseTransactions();
        ICollection<PurchaseTransaction> GetAllPurchaseTransactionsForUser(int userId);
        ICollection<PurchaseTransaction> GetAllPurchaseTransactionsForBrokerageAccount(int brokerageAccountId);

        ICollection<PurchaseTransaction> GetOpenPurchaseTransactionsInBrokerageAccount(int brokerageAccountId);
        ICollection<PurchaseTransaction> GetOpenPurchaseTransactionsInBrokerageAccountForSecurity(int brokerageAccountId, int securityId);

        ICollection<Stock> GetStocksWithOpenPositionsForUser(int userId);
        ICollection<Stock> GetStocksWithOpenPositionsForBrokerageAccount(int brokerageAccountId);

        DbOperationStatus InsertPurchaseTransaction(PurchaseTransaction purchaseTransaction);
        DbOperationStatus UpdatePurchaseTransaction(PurchaseTransaction purchaseTransaction);
        DbOperationStatus DeletePurchaseTransaction(PurchaseTransaction purchaseTransaction);
    }
}