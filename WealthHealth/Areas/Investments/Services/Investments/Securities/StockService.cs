using System;
using System.Collections.Generic;
using System.Linq;
using WealthHealth.Models.Investments;
using WealthHealth.Repositories;
using WealthHealth.Repositories.Investments;
using WealthHealth.ViewModels.Investments.Stocks;

namespace WealthHealth.Areas.Investments.Services.Investments.Securities
{
    public class StockService : IStockService
    {
        public readonly IStockRepository StockRepository;

        public StockService(
            IStockRepository stockRepository
        )
        {
            StockRepository = stockRepository;
        }

        public Stock GetStock(int id)
        {
            return StockRepository.GetStock(id);
        }

        public Stock GetStock(string symbol)
        {
            return StockRepository.GetStock(symbol);
        }

        public ICollection<Stock> GetAllStocks()
        {
            return StockRepository.GetAllStocks();
        }

        public int Create(AddStockVm addStockVm)
        {
            var stock = new Stock
                            {
                Title = addStockVm.Title,
                Symbol = addStockVm.Symbol,
            };

            DbOperationStatus opStatus = StockRepository.InsertStock(stock);
            if (opStatus.OperationSuccessStatus)
            {
                return opStatus.AffectedIndices.First();
            }
            return -1;
        }

        public bool Update(EditStockVm editStockVm, Stock stock)
        {
            if (editStockVm.Id == stock.Id)
            {
                stock.Symbol = editStockVm.Symbol;
                stock.Title = editStockVm.Title;

                return StockRepository.UpdateStock(stock).OperationSuccessStatus;
            }

            return false;
        }

        public bool ValidateDuplicateSymbol(string symbol)
        {
            var stock = StockRepository.GetStock(symbol);

            return stock != null;
        }


        public List<StockListVm> GetStockList()
        {
            var stocks = GetAllStocks();
            var stockList = stocks.Select(s =>
                                    new StockListVm
                                    {
                                        Id = s.Id,
                                        Title = String.Format("{0}", s.SecurityWithSymbol)
                                    }).ToList();

            return stockList;
        }
    }
}