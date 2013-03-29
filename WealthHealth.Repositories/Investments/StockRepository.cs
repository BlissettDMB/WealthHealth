using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public class StockRepository<TC> : BaseEntityRepository<TC, Stock>, IStockRepository where TC : DbContext, new()
    {
        public Stock GetStock(int stockId)
        {
            return GetQueryable().FirstOrDefault(b => b.Id == stockId);
        }

        public Stock GetStock(string symbol)
        {
            return GetQueryable().FirstOrDefault(b => b.Symbol == symbol);
        }

        public ICollection<Stock> GetAllStocks()
        {
            return GetOrderByAscending(b => b.Symbol).ToList();
        }

        public DbOperationStatus InsertStock(Stock stock)
        {
            var opStatus = new DbOperationStatus
            {
                OperationSuccessStatus = false,
                AffectedIndices = new List<int>()
            };

            try
            {
                DataContext.Set<Stock>().Add(stock);
                opStatus.OperationSuccessStatus = DataContext.SaveChanges() > 0;
                if (opStatus.OperationSuccessStatus)
                {
                    opStatus.AffectedIndices.Add(stock.Id);
                }
            }
            catch (Exception ex)
            {
                opStatus = DbOperationStatus.CreateFromException("Error inserting " + stock.GetType(), ex);
            }
            return opStatus;
        }

        public DbOperationStatus UpdateStock(Stock stock)
        {
            return UpdateEntity(stock);
        }

        public DbOperationStatus DeleteStock(Stock stock)
        {
            return Delete(stock);
        }
    }
}