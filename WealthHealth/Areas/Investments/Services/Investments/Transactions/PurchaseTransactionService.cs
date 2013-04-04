using System;
using System.Collections.Generic;
using System.Linq;
using WealthHealth.Models.Investments;
using WealthHealth.Repositories;
using WealthHealth.Repositories.Investments;
using WealthHealth.Services.StateManagement;
using WealthHealth.ViewModels.Investments.Transactions;

namespace WealthHealth.Areas.Investments.Services.Investments.Transactions
{
    public class PurchaseTransactionService : IPurchaseTransactionService
    {
        public readonly IPurchaseTransactionRepository PurchaseTransactionRepository;
        public readonly ActiveUserService ActiveUserService;

        public PurchaseTransactionService(
            IPurchaseTransactionRepository purchaseTransactionRepository
        )
        {
            PurchaseTransactionRepository = purchaseTransactionRepository;
            ActiveUserService = new ActiveUserService();
        }
            
        public int CreateStockPurchase(AddPurchaseTransactionVm addPurchaseTransactionVm)
        {
            var purchaseTransaction = new PurchaseTransaction()
            {
                BrokerageAccountId = addPurchaseTransactionVm.BrokerageAccountId,
                Commission = addPurchaseTransactionVm.Commission,
                SecurityId = addPurchaseTransactionVm.SecurityId,
                ShareQuantity = addPurchaseTransactionVm.ShareQuantity,
                UnitPrice = addPurchaseTransactionVm.UnitPrice,
                TransactionDate = addPurchaseTransactionVm.TransactionDate,
                ClosedShares = 0,
                RemainingShares = addPurchaseTransactionVm.ShareQuantity,
                PositionClosedStatus = false
            };

            DbOperationStatus opStatus = PurchaseTransactionRepository.InsertPurchaseTransaction(purchaseTransaction);
            if (opStatus.OperationSuccessStatus)
            {
                return opStatus.AffectedIndices.First();
            }
            return -1;
        }

        public PurchaseTransaction GetPurchaseTransaction(int purchaseTransactionId)
        {
            return PurchaseTransactionRepository.GetPurchaseTransaction(purchaseTransactionId);
        }

        public ICollection<SecurityPurchaseHistoryVm> GetPurchaseTransactionHistoryForActiveUser()
        {
            return GetPurchaseTransactionHistory(ActiveUserService.GetUserId());
        }

        public ICollection<SecurityPurchaseHistoryVm> GetPurchaseTransactionHistory(int userId)
        {
            var purchaseTransactions = PurchaseTransactionRepository.GetAllPurchaseTransactionsForUser(userId);
            var securityList = purchaseTransactions.Select(p => p.Security).OrderBy(s => s.SecurityWithSymbol).Distinct();

            return (from security in securityList
                                           let securityTransactions = purchaseTransactions.Where(t => t.SecurityId == security.Id)
                                           select new SecurityPurchaseHistoryVm
                                                      {
                                                          Security = security, 
                                                          PurchaseTransactions = securityTransactions.ToList(), 
                                                          TotalOpenShareQuantity = securityTransactions.Sum(t => t.RemainingShares),
                                                          DollarCost = securityTransactions.Sum(t => t.UnitPrice * t.RemainingShares)
                                                      }).ToList();
        }

        public ICollection<Stock> GetStocksWithOpenPositionsForActiveUser()
        {
            return GetStocksWithOpenPositionsForUser(ActiveUserService.GetUserId());
        }

        public ICollection<Stock> GetStocksWithOpenPositionsForUser(int userId)
        {
            return PurchaseTransactionRepository.GetStocksWithOpenPositionsForUser(userId);
        }

        public ICollection<Stock> GetStocksWithOpenPositionsForBrokerageAccount(int brokerageAccountId)
        {
            return PurchaseTransactionRepository.GetStocksWithOpenPositionsForBrokerageAccount(brokerageAccountId);
        }

        public ICollection<PurchaseTransaction> GetOpenPurchaseTransactionsInBrokerageAccount(int brokerageAccountId)
        {
            return PurchaseTransactionRepository.GetOpenPurchaseTransactionsInBrokerageAccount(brokerageAccountId);
        }

        public ICollection<PurchaseTransaction> GetOpenPurchaseTransactionsInBrokerageAccountForSecurity(int brokerageAccountId, int securityId)
        {
            return PurchaseTransactionRepository.GetOpenPurchaseTransactionsInBrokerageAccountForSecurity(brokerageAccountId, securityId);
        }
    }
}