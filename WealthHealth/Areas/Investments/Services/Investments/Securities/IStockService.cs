using System.Collections.Generic;
using WealthHealth.Models.Investments;
using WealthHealth.ViewModels.Investments.Stocks;

namespace WealthHealth.Areas.Investments.Services.Investments.Securities
{
    public interface IStockService
    {
        Stock GetStock(int id);
        Stock GetStock(string symbol);

        ICollection<Stock> GetAllStocks();

        int Create(AddStockVm addStockVm);
        bool Update(EditStockVm editStockVm, Stock stock);

        bool ValidateDuplicateSymbol(string symbol);

        List<StockListVm> GetStockList();
    }
}