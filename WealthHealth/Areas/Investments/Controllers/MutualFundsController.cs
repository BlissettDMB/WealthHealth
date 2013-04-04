using System.Web.Mvc;
using WealthHealth.Filters.StateManagement;
using WealthHealth.Helpers.Html.FlashMessages;
using WealthHealth.Areas.Investments.Services.Investments.Securities;
using WealthHealth.Services.StateManagement;
using WealthHealth.ViewModels.Investments.MutualFunds;

namespace WealthHealth.Areas.Investments.Controllers
{
    [Authorize]
    [ActiveUserRequired]
    public class MutualFundsController : Controller
    {
        public readonly IMutualFundService MutualFundService;
        public readonly ActiveUserService ActiveUserService;

        public MutualFundsController(
            IMutualFundService mutualFundService
        )
        {
            MutualFundService = mutualFundService;
            ActiveUserService = new ActiveUserService();
        }

        //
        // GET: /MutualFunds/

        public ActionResult Index()
        {
            var mutualFunds = MutualFundService.GetAllMutualFunds();
            return View(mutualFunds);
        }

        //
        // GET: /MutualFunds/Details/5

        public ActionResult Details(int id)
        {
            var mutualFund = MutualFundService.GetMutualFund(id);
            return View(mutualFund);
        }

        //
        // GET: /MutualFunds/Add

        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /MutualFunds/Add

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddMutualFundVm addMutualFundVm)
        {
            if (ModelState.IsValid)
            {
                if (!MutualFundService.ValidateDuplicateSymbol(addMutualFundVm.Symbol))
                {
                    int mutualFundId = MutualFundService.Create(addMutualFundVm);

                    if (mutualFundId > 0)
                    {
                        this.FlashSuccess("Successfully created the mutual fund.", "Details", "MutualFunds");
                        return RedirectToAction("Details", "MutualFunds", new { area = "", id = mutualFundId });
                    }
                    this.FlashError("Could not create the mutual fund. Please try again.", "Add", "MutualFunds");
                }
                else
                {
                    ModelState.AddModelError("Symbol", "The provided mutual fund already exists.");
                    this.FlashError("The provided mutual fund already exists.", "Add", "MutualFunds");
                }
            }

            return View(addMutualFundVm);
        }

        //
        // GET: /MutualFunds/Edit/5

        public ActionResult Edit(int id)
        {
            var mutualFund = MutualFundService.GetMutualFund(id);
            if (mutualFund == null)
            {
                this.FlashError("Invalid mutual fund requested. Please try again.", "Index", "MutualFunds");
                return RedirectToAction("Index", "MutualFunds", new { area = "" });
            }

            ViewBag.MutualFund = mutualFund;

            var editMutualFundVm = new EditMutualFundVm
            {
                Title = mutualFund.Title,
                Symbol = mutualFund.Symbol,
                Id = mutualFund.Id
            };

            return View(editMutualFundVm);
        }

        //
        // POST: /MutualFunds/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditMutualFundVm editMutualFundVm)
        {
            var mutualFund = MutualFundService.GetMutualFund(id);
            if (mutualFund == null)
            {
                this.FlashError("Invalid mutual fund requested. Please try again.", "Index", "MutualFunds");
                return RedirectToAction("Index", "MutualFunds", new { area = "" });
            }

            if (ModelState.IsValid)
            {
                bool symbolDiff = mutualFund.Symbol != editMutualFundVm.Symbol;
                if (!symbolDiff ||
                    (!MutualFundService.ValidateDuplicateSymbol(editMutualFundVm.Symbol)))
                {
                    bool updatedStock = MutualFundService.Update(editMutualFundVm, mutualFund);

                    if (updatedStock)
                    {
                        this.FlashSuccess("Successfully updated the mutual fund.", "Details", "MutualFunds");
                        return RedirectToAction("Details", "MutualFunds", new { area = "", id = mutualFund.Id });
                    }
                    this.FlashError("Could not update the mutual fund. Please try again.", "Edit", "MutualFunds");
                }
                else
                {
                    ModelState.AddModelError("Symbol", "The provided symbol already exists.");
                    this.FlashError("The provided symbol already exists.", "Edit", "MutualFunds");
                }

            }

            return View(editMutualFundVm);
        }
    }
}