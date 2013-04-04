using System.Collections.Generic;
using WealthHealth.Models.Investments;
using WealthHealth.ViewModels.Investments.Transactions;

namespace WealthHealth.Areas.Investments.Services.Investments.Transactions
{
    public interface IPurchaseTransactionService
    {
        int CreateStockPurchase(AddPurchaseTransactionVm addPurchaseTransactionVm);

        PurchaseTransaction GetPurchaseTransaction(int purchaseTransactionId);

        ICollection<SecurityPurchaseHistoryVm> GetPurchaseTransactionHistoryForActiveUser();
        ICollection<SecurityPurchaseHistoryVm> GetPurchaseTransactionHistory(int userId);

        ICollection<Stock> GetStocksWithOpenPositionsForActiveUser();
        ICollection<Stock> GetStocksWithOpenPositionsForUser(int userId);
        ICollection<Stock> GetStocksWithOpenPositionsForBrokerageAccount(int brokerageAccountId);

        ICollection<PurchaseTransaction> GetOpenPurchaseTransactionsInBrokerageAccount(int brokerageAccountId);
        ICollection<PurchaseTransaction> GetOpenPurchaseTransactionsInBrokerageAccountForSecurity(int brokerageAccountId, int securityId);
    }
}