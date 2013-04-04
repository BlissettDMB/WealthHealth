using System.Collections.Generic;
using WealthHealth.Models.Investments;
using WealthHealth.ViewModels.Investments.BrokerageAccounts;

namespace WealthHealth.Areas.Investments.Services.Investments.BrokerageAccounts
{
    public interface IBrokerageAccountService
    {
        BrokerageAccount GetBrokerageAccount(int brokerageAccountId);
        BrokerageAccount GetBrokerageAccount(string accountNumber);

        Brokerage GetBrokerage(int brokerageId);

        ICollection<Brokerage> GetAllBrokerages();
        List<BrokerageListVm> GetBrokerageList();

        ICollection<BrokerageAccount> GetAllBrokerageAccounts();
        ICollection<BrokerageAccount> GetAllRetirementAccounts();
        ICollection<BrokerageAccount> GetAllNonRetirementAccounts();

        BrokerageAccount GetBrokerageAccountForActiveUser(int brokerageAccountId);
        BrokerageAccount GetBrokerageAccountForUser(int brokerageAccountId, int userId);
        ICollection<BrokerageAccount> GetAllBrokerageAccountsForUser(int userId);
        ICollection<BrokerageAccount> GetAllRetirementAccountsForUser(int userId);
        ICollection<BrokerageAccount> GetAllNonRetirementAccountsForUser(int userId);
        List<BrokerageAccountListVm> GetBrokerageAccountListForActiveUser();
        List<BrokerageAccountListVm> GetBrokerageAccountList(int userId);

        BrokerageAccountOverviewVm GetBrokerageAccountOverviewForActiveUser();
        BrokerageAccountOverviewVm GetBrokerageAccountOverviewForUser(int userId);

        decimal GetBrokerageAccountTotalValue(int brokerageAccountId);
        decimal GetBrokerageAccountTotalPositionValue(int brokerageAccountId);
        decimal GetBrokerageAccountTotalStockPositionValue(int brokerageAccountId);
        decimal GetBrokerageAccountTotalMutualFundValue(int brokerageAccountId);
        decimal GetBrokerageAccountTotalCashValue(int brokerageAccountId);

        bool ValidateDuplicateAccountNumber(AddBrokerageAccountVm addBrokerageAccountVm);
        bool ValidateDuplicateAccountNumber(string accountNumber, int brokerageId);

        int CreateBrokerageAccount(AddBrokerageAccountVm addBrokerageAccountVm);
        bool UpdateBrokerageAccount(EditBrokerageAccountVm editBrokerageAccountVm, BrokerageAccount brokerageAccount);

        int CreateBrokerage(AddBrokerageVm addBrokerageVm);

        bool ValidateDuplicateBrokerageTitle(string brokerageTitle);
    }
}