using System.Collections.Generic;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public interface IStockRepository : IBaseEntityRepository<Stock>
    {
        Stock GetStock(int stockId);
        Stock GetStock(string symbol);

        ICollection<Stock> GetAllStocks();

        DbOperationStatus InsertStock(Stock stock);
        DbOperationStatus UpdateStock(Stock stock);
        DbOperationStatus DeleteStock(Stock stock);
    }
}