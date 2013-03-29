using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public class SellTransactionRepository<TC> : BaseEntityRepository<TC, SellTransaction>, ISellTransactionRepository where TC : DbContext, new()
    {
        public SellTransaction GetSellTransaction(int sellTransactionId)
        {
            return GetQueryable().FirstOrDefault(t => t.Id == sellTransactionId);
        }

        public ICollection<SellTransaction> GetAllSellTransactions()
        {
            return GetOrderByDescending(t => t.TransactionDate.ToShortDateString()).ToList();
        }

        public DbOperationStatus InsertSellTransaction(SellTransaction sellTransaction)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = new List<int>()
            };

            try
            {
                DataContext.Set<SellTransaction>().Add(sellTransaction);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
                if (opStatus.OperationSuccessStatus)
                {
                    opStatus.AffectedIndices.Add(sellTransaction.Id);
                }
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error inserting " + sellTransaction.GetType(), ex);
            }
            return opStatus;
        }

        public DbOperationStatus UpdateSellTransaction(SellTransaction sellTransaction)
        {
            return Update(sellTransaction);
        }

        public DbOperationStatus DeleteSellTransaction(SellTransaction sellTransaction)
        {
            return Delete(sellTransaction);
        }
    }
}