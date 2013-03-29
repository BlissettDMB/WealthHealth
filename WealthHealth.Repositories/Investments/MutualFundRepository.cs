using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public class MutualFundRepository<TC> : BaseEntityRepository<TC, MutualFund>, IMutualFundRepository where TC : DbContext, new()
    {
        public MutualFund GetMutualFund(int mutualFundId)
        {
            return GetQueryable().FirstOrDefault(b => b.Id == mutualFundId);
        }

        public MutualFund GetMutualFund(string symbol)
        {
            return GetQueryable().FirstOrDefault(b => b.Symbol == symbol);
        }

        public ICollection<MutualFund> GetAllMutualFunds()
        {
            return GetOrderByAscending(b => b.Symbol).ToList();
        }

        public DbOperationStatus InsertMutualFund(MutualFund mutualFund)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = new List<int>()
            };

            try
            {
                DataContext.Set<MutualFund>().Add(mutualFund);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
                if (opStatus.OperationSuccessStatus)
                {
                    opStatus.AffectedIndices.Add(mutualFund.Id);
                }
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error inserting " + mutualFund.GetType(), ex);
            }
            return opStatus;
        }

        public DbOperationStatus UpdateMutualFund(MutualFund mutualFund)
        {
            return Update(mutualFund);
        }

        public DbOperationStatus DeleteMutualFund(MutualFund mutualFund)
        {
            return Delete(mutualFund);
        }
    }
}