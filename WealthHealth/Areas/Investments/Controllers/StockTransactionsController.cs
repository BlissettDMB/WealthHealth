using System.Linq;
using System.Web.Mvc;
using WealthHealth.Filters.StateManagement;
using WealthHealth.Helpers.Html.FlashMessages;
using WealthHealth.Areas.Investments.Services.Investments.BrokerageAccounts;
using WealthHealth.Areas.Investments.Services.Investments.Securities;
using WealthHealth.Areas.Investments.Services.Investments.Transactions;
using WealthHealth.Services.StateManagement;
using WealthHealth.ViewModels.Investments.Transactions;

namespace WealthHealth.Areas.Investments.Controllers
{
    [Authorize]
    [ActiveUserRequired]
    public class StockTransactionsController : Controller
    {
        public readonly IPurchaseTransactionService PurchaseTransactionService;
        public readonly ISellTransactionService SellTransactionService;
        public readonly IBrokerageAccountService BrokerageAccountService;
        public readonly IStockService StockService;
        public readonly ActiveUserService ActiveUserService;

        public StockTransactionsController(
            IPurchaseTransactionService purchaseTransactionService,
            ISellTransactionService sellTransactionService,
            IBrokerageAccountService brokerageAccountService,
            IStockService stockService
        )
        {
            PurchaseTransactionService = purchaseTransactionService;
            SellTransactionService = sellTransactionService;
            BrokerageAccountService = brokerageAccountService;
            StockService = stockService;
            ActiveUserService = new ActiveUserService();
        }

        //
        // GET: /StockTransactions/AddPurchase

        public ActionResult AddPurchase()
        {
            var brokerageAccountList = BrokerageAccountService.GetBrokerageAccountListForActiveUser();
            ViewBag.BrokerageAccountId = new SelectList(brokerageAccountList, "Id", "Title");

            var stockList = StockService.GetStockList();
            ViewBag.SecurityId = new SelectList(stockList, "Id", "Title");

            return View();
        }

        //
        // POST: /StockTransactions/AddPurchase

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPurchase(AddPurchaseTransactionVm addPurchaseTransactionVm)
        {
            if (ModelState.IsValid)
            {
                int stockTransactionId = PurchaseTransactionService.CreateStockPurchase(addPurchaseTransactionVm);

                if (stockTransactionId > 0)
                {
                    this.FlashSuccess("Successfully created the stock transaction.", "PurchaseDetails", "StockTransactions");
                    return RedirectToAction("PurchaseDetails", "StockTransactions", new { area = "", id = stockTransactionId });
                }
                this.FlashError("Could not create the stock transaction. Please try again.", "AddPurchase", "StockTransactions");
            }

            var brokerageAccountList = BrokerageAccountService.GetBrokerageAccountListForActiveUser();
            ViewBag.BrokerageAccountId = new SelectList(brokerageAccountList, "Id", "Title", addPurchaseTransactionVm.BrokerageAccountId);

            var stockList = StockService.GetStockList();
            ViewBag.SecurityId = new SelectList(stockList, "Id", "Title", addPurchaseTransactionVm.SecurityId);

            return View();
        }

        //
        // GET: /StockTransactions/AddSell

        public ActionResult AddSell()
        {
            var brokerageAccountList = BrokerageAccountService.GetBrokerageAccountListForActiveUser();

            // Does the active user have any brokerage accounts setup?
            if (!brokerageAccountList.Any())
            {
                this.FlashError("Please ensure a brokerage account is setup and try again.", "Index", "BrokerageAccounts");
                return RedirectToAction("Index", "BrokerageAccounts", new { area = "" });
            }

            ViewBag.BrokerageAccountId = new SelectList(brokerageAccountList, "Id", "Title");

            var stockList = PurchaseTransactionService.GetStocksWithOpenPositionsForBrokerageAccount(brokerageAccountList.First().Id);
            ViewBag.SecurityId = new SelectList(stockList, "Id", "Title");

            return View();
        }

        //
        // POST: /StockTransactions/AddSell

        [HttpPost]
        public ActionResult AddSell(AddSellTransactionVm addSellTransactionVm)
        {
            if (ModelState.IsValid)
            {
                var brokerageAccount =
                    BrokerageAccountService.GetBrokerageAccount(addSellTransactionVm.BrokerageAccountId);
                // Did we find the requested account and does it belong to the active user?
                if (brokerageAccount == null || brokerageAccount.UserId != ActiveUserService.GetUserId())
                {
                    this.FlashError("Please ensure the requested brokerage account is setup under the active user and try again.", "Index", "BrokerageAccounts");
                    return RedirectToAction("Index", "BrokerageAccounts", new { area = "" });
                }

                int sellTransactionId = SellTransactionService.CreateSellTransaction(addSellTransactionVm);

                if (sellTransactionId > 0)
                {
                    this.FlashSuccess("Successfully created the sell transaction.", "SellDetails", "StockTransactions");
                    return RedirectToAction("SellDetails", "StockTransactions", new { area = "", id = sellTransactionId });
                }
                this.FlashError("Could not create the sell transaction. Please try again.", "AddSell", "StockTransactions");
            }

            foreach (var transactionMatch in addSellTransactionVm.MatchingPurchaseTransactions)
            {
                transactionMatch.PurchaseTransaction =
                    PurchaseTransactionService.GetPurchaseTransaction(transactionMatch.PurchaseTransactionId);
            }

            var brokerageAccountList = BrokerageAccountService.GetBrokerageAccountListForActiveUser();

            // Does the active user have any brokerage accounts setup?
            if (!brokerageAccountList.Any())
            {
                this.FlashError("Please ensure a brokerage account is setup and try again.", "Index", "BrokerageAccounts");
                return RedirectToAction("Index", "BrokerageAccounts", new { area = "" });
            }

            ViewBag.BrokerageAccountId = new SelectList(brokerageAccountList, "Id", "Title");

            var stockList = PurchaseTransactionService.GetStocksWithOpenPositionsForBrokerageAccount(addSellTransactionVm.BrokerageAccountId);
            ViewBag.SecurityId = new SelectList(stockList, "Id", "Title", addSellTransactionVm.SecurityId);

            return View(addSellTransactionVm);
        }

        //
        // GET: /StockTransactions/PurchaseDetails

        public ActionResult PurchaseDetails(int id)
        {
            var stockPurchaseTransaction = PurchaseTransactionService.GetPurchaseTransaction(id);
            return View(stockPurchaseTransaction);
        }
    }
}