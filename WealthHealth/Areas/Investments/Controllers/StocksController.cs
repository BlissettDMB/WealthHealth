using System.Web.Mvc;
using WealthHealth.Filters.StateManagement;
using WealthHealth.Helpers.Html.FlashMessages;
using WealthHealth.Areas.Investments.Services.Investments.Securities;
using WealthHealth.Services.StateManagement;
using WealthHealth.ViewModels.Investments.Stocks;

namespace WealthHealth.Areas.Investments.Controllers
{
    [Authorize]
    [ActiveUserRequired]
    public class StocksController : Controller
    {
        public readonly IStockService StockService;
        public readonly ActiveUserService ActiveUserService;

        public StocksController(
            IStockService stockService
        )
        {
            StockService = stockService;
            ActiveUserService = new ActiveUserService();
        }

        //
        // GET: /Stocks/

        public ActionResult Index()
        {
            var stocks = StockService.GetAllStocks();
            return View(stocks);
        }

        //
        // GET: /Stocks/Details/5

        public ActionResult Details(int id)
        {
            var stock = StockService.GetStock(id);
            return View(stock);
        }

        //
        // GET: /Stocks/Add

        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /Stocks/Add

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddStockVm addStockVm)
        {
            if (ModelState.IsValid)
            {
                if (!StockService.ValidateDuplicateSymbol(addStockVm.Symbol))
                {
                    int stockId = StockService.Create(addStockVm);

                    if (stockId > 0)
                    {
                        this.FlashSuccess("Successfully created the stock.", "Details", "Stocks");
                        return RedirectToAction("Details", "Stocks", new { area = "", id = stockId });
                    }
                    this.FlashError("Could not create the stock. Please try again.", "Add", "Stocks");
                }
                else
                {
                    ModelState.AddModelError("Symbol", "The provided stock already exists.");
                    this.FlashError("The provided stock already exists.", "Add", "Stocks");
                }
            }

            return View(addStockVm);
        }

        //
        // GET: /Stocks/Edit/5

        public ActionResult Edit(int id)
        {
            var stock = StockService.GetStock(id);
            if (stock == null)
            {
                this.FlashError("Invalid stock requested. Please try again.", "Index", "Stocks");
                return RedirectToAction("Index", "Stocks", new { area = "" });
            }

            ViewBag.Stock = stock;

            var editStockVm = new EditStockVm
            {
                Title = stock.Title,
                Symbol = stock.Symbol,
                Id = stock.Id
            };

            return View(editStockVm);
        }

        //
        // POST: /Stocks/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditStockVm editStockVm)
        {
            var stock = StockService.GetStock(id);
            if (stock == null)
            {
                this.FlashError("Invalid stock requested. Please try again.", "Index", "Stocks");
                return RedirectToAction("Index", "Stocks", new { area = "" });
            }

            if (ModelState.IsValid)
            {
                bool symbolDiff = stock.Symbol != editStockVm.Symbol;
                if (!symbolDiff ||
                    (!StockService.ValidateDuplicateSymbol(editStockVm.Symbol)))
                {
                    bool updatedStock = StockService.Update(editStockVm, stock);

                    if (updatedStock)
                    {
                        this.FlashSuccess("Successfully updated the stock.", "Details", "Stocks");
                        return RedirectToAction("Details", "Stocks", new { area = "", id = stock.Id });
                    }
                    this.FlashError("Could not update the stock. Please try again.", "Edit", "Stocks");
                }
                else
                {
                    ModelState.AddModelError("Symbol", "The provided symbol already exists.");
                    this.FlashError("The provided symbol already exists.", "Edit", "Stocks");
                }

            }

            return View(editStockVm);
        }
    }
}