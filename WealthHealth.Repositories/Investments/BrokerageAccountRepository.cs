using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public class BrokerageAccountRepository<TC> : BaseEntityRepository<TC, BrokerageAccount>, IBrokerageAccountRepository where TC : DbContext, new()
    {
        public BrokerageAccount GetBrokerageAccount(int brokerageAccountId)
        {
            return GetQueryable().Include(b => b.Brokerage).FirstOrDefault(b => b.Id == brokerageAccountId);
        }

        public BrokerageAccount GetBrokerageAccount(string accountNumber)
        {
            return GetQueryable().Include(b => b.Brokerage).FirstOrDefault(b => b.AccountNumber == accountNumber);
        }

        public ICollection<BrokerageAccount> GetAllBrokerageAccounts()
        {
            return GetOrderByAscending(b => b.AccountNumber).Include(b => b.Brokerage).ToList();
        }

        public ICollection<BrokerageAccount> GetAllRetirementAccounts()
        {
            return GetOrderByAscending(b => b.IsRetirement, b => b.AccountNumber).Include(b => b.Brokerage).ToList();
        }

        public ICollection<BrokerageAccount> GetAllNonRetirementAccounts()
        {
            return GetOrderByAscending(b => !b.IsRetirement, b => b.AccountNumber).Include(b => b.Brokerage).ToList();
        }

        public BrokerageAccount GetBrokerageAccountForUser(int brokerageAccountId, int userId)
        {
            return GetQueryable().Include(b => b.Brokerage).FirstOrDefault(b => b.Id == brokerageAccountId && b.UserId == userId);
        }

        public ICollection<BrokerageAccount> GetAllBrokerageAccountsForUser(int userId)
        {
            return GetOrderByAscending(b => b.UserId == userId, b => b.AccountNumber).Include(b => b.Brokerage).ToList();
        }

        public ICollection<BrokerageAccount> GetAllRetirementAccountsForUser(int userId)
        {
            return GetOrderByAscending(b => b.IsRetirement && b.UserId == userId, b => b.AccountNumber).Include(b => b.Brokerage).ToList();
        }

        public ICollection<BrokerageAccount> GetAllNonRetirementAccountsForUser(int userId)
        {
            return GetOrderByAscending(b => !b.IsRetirement && b.UserId == userId, b => b.AccountNumber).Include(b => b.Brokerage).ToList();
        }

        public Brokerage GetBrokerage(int brokerageId)
        {
            return DataContext.Set<Brokerage>().FirstOrDefault(b => b.Id == brokerageId);
        }

        public Brokerage GetBrokerage(string brokerageTitle)
        {
            return DataContext.Set<Brokerage>().FirstOrDefault(b => b.Title == brokerageTitle);
        }

        public ICollection<Brokerage> GetAllBrokerages()
        {
            return DataContext.Set<Brokerage>().OrderBy(b => b.Title).ToList();
        }

        public ICollection<BrokerageAccount> GetAllBrokerageAccountsForBrokerage(int brokerageId)
        {
            return GetOrderByAscending(b => b.BrokerageId == brokerageId, b => b.AccountNumber).ToList();
        }

        public ICollection<BrokerageAccount> GetAllRetirementAccountsForBrokerage(int brokerageId)
        {
            return GetOrderByAscending(b => b.IsRetirement && b.BrokerageId == brokerageId, b => b.AccountNumber).ToList();
        }

        public ICollection<BrokerageAccount> GetAllNonRetirementAccountsForBrokerage(int brokerageId)
        {
            return GetOrderByAscending(b => !b.IsRetirement && b.BrokerageId == brokerageId, b => b.AccountNumber).ToList();
        }

        public ICollection<BrokerageAccount> GetAllBrokerageAccountsForUserForBrokerage(int userId, int brokerageId)
        {
            return GetOrderByAscending(b => b.UserId == userId && b.BrokerageId == brokerageId, b => b.AccountNumber).ToList();
        }

        public ICollection<BrokerageAccount> GetAllRetirementAccountsForUserForBrokerage(int userId, int brokerageId)
        {
            return GetOrderByAscending(b => b.IsRetirement && b.BrokerageId == brokerageId && b.UserId == userId, b => b.AccountNumber).ToList();
        }

        public ICollection<BrokerageAccount> GetAllNonRetirementAccountsForUserForBrokerage(int userId, int brokerageId)
        {
            return GetOrderByAscending(b => !b.IsRetirement && b.BrokerageId == brokerageId && b.UserId == userId, b => b.AccountNumber).ToList();
        }

        public DbOperationStatus InsertBrokerage(Brokerage brokerage)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = new List<int>()
            };

            try
            {
                DataContext.Set<Brokerage>().Add(brokerage);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
                if (opStatus.OperationSuccessStatus)
                {
                    opStatus.AffectedIndices.Add(brokerage.Id);
                }
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error inserting " + brokerage.GetType(), ex);
            }
            return opStatus;
        }

        public DbOperationStatus UpdateBrokerage(Brokerage brokerage)
        {
            return UpdateEntity<Brokerage>(brokerage);
        }

        public DbOperationStatus DeleteBrokerage(Brokerage brokerage)
        {
            return DeleteEntity<Brokerage>(brokerage);
        }

        public DbOperationStatus InsertBrokerageAccount(BrokerageAccount brokerageAccount)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = new List<int>()
            };

            try
            {
                DataContext.Set<BrokerageAccount>().Add(brokerageAccount);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
                if (opStatus.OperationSuccessStatus)
                {
                    opStatus.AffectedIndices.Add(brokerageAccount.Id);
                }
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error inserting " + brokerageAccount.GetType(), ex);
            }
            return opStatus;
        }

        public DbOperationStatus UpdateBrokerageAccount(BrokerageAccount brokerageAccount)
        {
            return Update(brokerageAccount);
        }

        public DbOperationStatus DeleteBrokerageAccount(BrokerageAccount brokerageAccount)
        {
            return Delete(brokerageAccount);
        }
    }
}