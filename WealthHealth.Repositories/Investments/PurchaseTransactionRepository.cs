using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public class PurchaseTransactionRepository<TC> : BaseEntityRepository<TC, PurchaseTransaction>, IPurchaseTransactionRepository where TC : DbContext, new()
    {
        public PurchaseTransaction GetPurchaseTransaction(int purchaseTransactionId)
        {
            return GetQueryable().FirstOrDefault(t => t.Id == purchaseTransactionId);
        }

        public ICollection<PurchaseTransaction> GetAllPurchaseTransactions()
        {
            return GetQueryable().OrderByDescending(t => t.TransactionDate).ToList();
        }

        public ICollection<PurchaseTransaction> GetAllPurchaseTransactionsForUser(int userId)
        {
            return GetQueryable()
                .Include(t => t.BrokerageAccount)
                .Include(t => t.Security)
                .Include(t => t.ClosingTransactionMatches)
                .Include(t => t.ClosingTransactionMatches.Select(c => c.SellTransaction))
                .Where(t => t.BrokerageAccount.UserId == userId)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }

        public ICollection<PurchaseTransaction> GetAllPurchaseTransactionsForBrokerageAccount(int brokerageAccountId)
        {
            return GetQueryable()
                .Include(t => t.Security)
                .Include(t => t.ClosingTransactionMatches)
                .Include(t => t.ClosingTransactionMatches.Select(c => c.SellTransaction))
                .Where(t => t.BrokerageAccountId == brokerageAccountId)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }

        public ICollection<PurchaseTransaction> GetOpenPurchaseTransactionsInBrokerageAccount(int brokerageAccountId)
        {
            return GetQueryable()
                .Include(t => t.Security)
                .Where(t => t.BrokerageAccountId == brokerageAccountId && t.RemainingShares > 0 && t.PositionClosedStatus == false)
                .OrderBy(t => t.TransactionDate)
                .ToList();
        }

        public ICollection<PurchaseTransaction> GetOpenPurchaseTransactionsInBrokerageAccountForSecurity(int brokerageAccountId, int securityId)
        {
            return GetQueryable()
                .Include(t => t.Security)
                .Where(t => t.BrokerageAccountId == brokerageAccountId && t.RemainingShares > 0 && t.PositionClosedStatus == false && t.SecurityId == securityId)
                .OrderBy(t => t.TransactionDate)
                .ToList();
        }

        public ICollection<Stock> GetStocksWithOpenPositionsForUser(int userId)
        {
            return GetQueryable()
                .Include(t => t.Security)
                .Where(t => t.BrokerageAccount.UserId == userId)
                .Select(t => t.Security)
                .OfType<Stock>()
                .Distinct()
                .OrderBy(s => s.Title)
                .ToList();
        }

        public ICollection<Stock> GetStocksWithOpenPositionsForBrokerageAccount(int brokerageAccountId)
        {
            return GetQueryable()
                .Include(t => t.Security)
                .Where(t => t.BrokerageAccountId == brokerageAccountId)
                .Select(t => t.Security)
                .OfType<Stock>()
                .Distinct()
                .OrderBy(s => s.Title)
                .ToList();
        }

        public DbOperationStatus InsertPurchaseTransaction(PurchaseTransaction purchaseTransaction)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = new List<int>()
            };

            try
            {
                DataContext.Set<PurchaseTransaction>().Add(purchaseTransaction);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
                if (opStatus.OperationSuccessStatus)
                {
                    opStatus.AffectedIndices.Add(purchaseTransaction.Id);
                }
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error inserting " + purchaseTransaction.GetType(), ex);
            }
            return opStatus;
        }

        public DbOperationStatus UpdatePurchaseTransaction(PurchaseTransaction purchaseTransaction)
        {
            return Update(purchaseTransaction);
        }

        public DbOperationStatus DeletePurchaseTransaction(PurchaseTransaction purchaseTransaction)
        {
            return Delete(purchaseTransaction);
        }
    }
}