using System.Web.Mvc;
using WealthHealth.Filters.StateManagement;
using WealthHealth.Helpers.Html.FlashMessages;
using WealthHealth.Areas.Investments.Services.Investments.BrokerageAccounts;
using WealthHealth.Services.StateManagement;
using WealthHealth.ViewModels.Investments.BrokerageAccounts;

namespace WealthHealth.Areas.Investments.Controllers
{
    [Authorize]
    [ActiveUserRequired]
    public class BrokerageAccountsController : Controller
    {
        public readonly IBrokerageAccountService BrokerageAccountService;
        public readonly ActiveUserService ActiveUserService;

        public BrokerageAccountsController(
            IBrokerageAccountService brokerageAccountService
        )
        {
            BrokerageAccountService = brokerageAccountService;
            ActiveUserService = new ActiveUserService();
        }

        //
        // GET: /BrokerageAccounts/

        public ActionResult Index()
        {
            var brokerageAccountOverview = BrokerageAccountService.GetBrokerageAccountOverviewForActiveUser();

            return View(brokerageAccountOverview);
        }

        //
        // GET: /BrokerageAccounts/Brokerages

        public ActionResult Brokerages()
        {
            var brokerages = BrokerageAccountService.GetAllBrokerages();
            return View(brokerages);
        }

        //
        // GET: /BrokerageAccounts/Details/5

        public ActionResult Details(int id)
        {
            var brokerageAccount = BrokerageAccountService.GetBrokerageAccountForUser(id, ActiveUserService.GetUserId());
            return View(brokerageAccount);
        }

        //
        // GET: /BrokerageAccounts/Add

        public ActionResult Add()
        {
            var brokerageList = BrokerageAccountService.GetBrokerageList();
            ViewBag.BrokerageId = new SelectList(brokerageList, "Id", "Title");

            return View();
        }

        //
        // POST: /BrokerageAccounts/Add

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddBrokerageAccountVm addBrokerageAccountVm)
        {
            if (ModelState.IsValid)
            {
                if (!BrokerageAccountService.ValidateDuplicateAccountNumber(addBrokerageAccountVm))
                {
                    int brokerageAccountId = BrokerageAccountService.CreateBrokerageAccount(addBrokerageAccountVm);

                    if (brokerageAccountId > 0)
                    {
                        this.FlashSuccess("Successfully created the brokerage account.", "Details", "BrokerageAccounts");
                        return RedirectToAction("Details", "BrokerageAccounts", new { area = "", id = brokerageAccountId });
                    }
                    this.FlashError("Could not create the brokerage account. Please try again.", "Add", "BrokerageAccounts");
                }
                else
                {
                    ModelState.AddModelError("AccountNumber", "The provided brokerage account already exists.");
                    this.FlashError("The provided brokerage account already exists.", "Add", "BrokerageAccounts");
                }
            }

            var brokerageList = BrokerageAccountService.GetBrokerageList();
            ViewBag.BrokerageId = new SelectList(brokerageList, "Id", "Title", addBrokerageAccountVm.BrokerageId);

            return View(addBrokerageAccountVm);
        }

        //
        // GET: /BrokerageAccounts/AddBrokerage

        public ActionResult AddBrokerage()
        {
            return View();
        }

        //
        // POST: /BrokerageAccounts/AddBrokerage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBrokerage(AddBrokerageVm addBrokerageVm)
        {
            if (ModelState.IsValid)
            {
                if (!BrokerageAccountService.ValidateDuplicateBrokerageTitle(addBrokerageVm.Title))
                {
                    int brokerageId = BrokerageAccountService.CreateBrokerage(addBrokerageVm);

                    if (brokerageId > 0)
                    {
                        this.FlashSuccess("Successfully created the brokerage.", "Brokerages", "BrokerageAccounts");
                        return RedirectToAction("Brokerages", "BrokerageAccounts", new { area = "", id = brokerageId });
                    }
                    this.FlashError("Could not create the brokerage. Please try again.", "AddBrokerage", "BrokerageAccounts");
                }
                else
                {
                    ModelState.AddModelError("Title", "The provided brokerage already exists.");
                    this.FlashError("The provided brokerage already exists.", "AddBrokerage", "BrokerageAccounts");
                }
            }

            return View(addBrokerageVm);
        }

        //
        // GET: /BrokerageAccounts/Edit/5

        public ActionResult Edit(int id)
        {
            var brokerageAccount = BrokerageAccountService.GetBrokerageAccountForActiveUser(id);
            if (brokerageAccount == null)
            {
                this.FlashError("Invalid brokerage account requested. Please try again.", "Index", "BrokerageAccounts");
                return RedirectToAction("Index", "BrokerageAccounts", new { area = "" });
            }

            ViewBag.BrokerageAccount = brokerageAccount;

            var brokerageList = BrokerageAccountService.GetBrokerageList();
            ViewBag.BrokerageId = new SelectList(brokerageList, "Id", "Title", brokerageAccount.BrokerageId);

            var editBrokerageAccountVm = new EditBrokerageAccountVm
            {
                IsRetirement = brokerageAccount.IsRetirement,
                Title = brokerageAccount.Title,
                BrokerageId = brokerageAccount.BrokerageId,
                AccountNumber = brokerageAccount.AccountNumber,
                UserId = brokerageAccount.UserId,
                Id = brokerageAccount.Id
            };

            return View(editBrokerageAccountVm);
        }

        //
        // POST: /BrokerageAccounts/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditBrokerageAccountVm editBrokerageAccountVm)
        {
            var brokerageAccount = BrokerageAccountService.GetBrokerageAccountForActiveUser(id);
            if (brokerageAccount == null)
            {
                this.FlashError("Invalid brokerage account requested. Please try again.", "Index", "BrokerageAccounts");
                return RedirectToAction("Index", "BrokerageAccounts", new { area = "" });
            }

            if (ModelState.IsValid)
            {
                bool accountNumberDiff = brokerageAccount.AccountNumber != editBrokerageAccountVm.AccountNumber;
                if (!accountNumberDiff ||
                    (!BrokerageAccountService.ValidateDuplicateAccountNumber(editBrokerageAccountVm.AccountNumber, editBrokerageAccountVm.BrokerageId)))
                {
                    bool updatedAccount = BrokerageAccountService.UpdateBrokerageAccount(editBrokerageAccountVm, brokerageAccount);

                    if (updatedAccount)
                    {
                        this.FlashSuccess("Successfully updated the brokerage account.", "Details", "BrokerageAccounts");
                        return RedirectToAction("Details", "BrokerageAccounts", new { area = "", id = brokerageAccount.Id });
                    }
                    this.FlashError("Could not update the brokerage account. Please try again.", "Edit", "BrokerageAccounts");
                }
                else
                {
                    ModelState.AddModelError("AccountNumber", "The provided account number already exists.");
                    this.FlashError("The provided account number already exists.", "Edit", "BrokerageAccounts");
                }

            }

            var brokerageList = BrokerageAccountService.GetBrokerageList();
            ViewBag.BrokerageId = new SelectList(brokerageList, "Id", "Title", brokerageAccount.BrokerageId);

            return View(editBrokerageAccountVm);
        }
    }
}