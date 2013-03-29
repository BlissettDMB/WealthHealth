using System.Collections.Generic;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public interface IBrokerageAccountRepository : IBaseEntityRepository<BrokerageAccount>
    {
        BrokerageAccount GetBrokerageAccount(int brokerageAccountId);
        BrokerageAccount GetBrokerageAccount(string accountNumber);

        ICollection<BrokerageAccount> GetAllBrokerageAccounts();
        ICollection<BrokerageAccount> GetAllRetirementAccounts();
        ICollection<BrokerageAccount> GetAllNonRetirementAccounts();

        BrokerageAccount GetBrokerageAccountForUser(int brokerageAccountId, int userId);
        ICollection<BrokerageAccount> GetAllBrokerageAccountsForUser(int userId);
        ICollection<BrokerageAccount> GetAllRetirementAccountsForUser(int userId);
        ICollection<BrokerageAccount> GetAllNonRetirementAccountsForUser(int userId);

        Brokerage GetBrokerage(int brokerageId);
        Brokerage GetBrokerage(string brokerageTitle);

        ICollection<Brokerage> GetAllBrokerages();

        ICollection<BrokerageAccount> GetAllBrokerageAccountsForBrokerage(int brokerageId);
        ICollection<BrokerageAccount> GetAllRetirementAccountsForBrokerage(int brokerageId);
        ICollection<BrokerageAccount> GetAllNonRetirementAccountsForBrokerage(int brokerageId);

        ICollection<BrokerageAccount> GetAllBrokerageAccountsForUserForBrokerage(int userId, int brokerageId);
        ICollection<BrokerageAccount> GetAllRetirementAccountsForUserForBrokerage(int userId, int brokerageId);
        ICollection<BrokerageAccount> GetAllNonRetirementAccountsForUserForBrokerage(int userId, int brokerageId);

        DbOperationStatus InsertBrokerage(Brokerage brokerage);
        DbOperationStatus UpdateBrokerage(Brokerage brokerage);
        DbOperationStatus DeleteBrokerage(Brokerage brokerage);

        DbOperationStatus InsertBrokerageAccount(BrokerageAccount brokerageAccount);
        DbOperationStatus UpdateBrokerageAccount(BrokerageAccount brokerageAccount);
        DbOperationStatus DeleteBrokerageAccount(BrokerageAccount brokerageAccount);
    }
}