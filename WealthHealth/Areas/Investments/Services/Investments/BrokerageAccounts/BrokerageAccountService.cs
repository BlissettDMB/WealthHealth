using System;
using System.Collections.Generic;
using System.Linq;
using WealthHealth.Models.Investments;
using WealthHealth.Repositories;
using WealthHealth.Repositories.Investments;
using WealthHealth.Services.StateManagement;
using WealthHealth.ViewModels.Investments.BrokerageAccounts;

namespace WealthHealth.Areas.Investments.Services.Investments.BrokerageAccounts
{
    public class BrokerageAccountService : IBrokerageAccountService, IDisposable
    {
        public readonly IBrokerageAccountRepository BrokerageAccountRepository;
        public readonly ActiveUserService ActiveUserService;

        public BrokerageAccountService(
            IBrokerageAccountRepository brokerageAccountRepository
        )
        {
            BrokerageAccountRepository = brokerageAccountRepository;
            ActiveUserService = new ActiveUserService();
        }

        public BrokerageAccount GetBrokerageAccount(int brokerageAccountId)
        {
            return BrokerageAccountRepository.GetBrokerageAccount(brokerageAccountId);
        }

        public BrokerageAccount GetBrokerageAccount(string accountNumber)
        {
            return BrokerageAccountRepository.GetBrokerageAccount(accountNumber);
        }

        public Brokerage GetBrokerage(int brokerageId)
        {
            return BrokerageAccountRepository.GetBrokerage(brokerageId);
        }

        public ICollection<Brokerage> GetAllBrokerages()
        {
            return BrokerageAccountRepository.GetAllBrokerages();
        }

        public List<BrokerageListVm> GetBrokerageList()
        {
            var brokerages = GetAllBrokerages();
            var brokerageList = brokerages.Select(b =>
                                    new BrokerageListVm
                                    {
                                        Id = b.Id,
                                        Title = b.Title
                                    }).ToList();

            return brokerageList;
        }

        public ICollection<BrokerageAccount> GetAllBrokerageAccounts()
        {
            return BrokerageAccountRepository.GetAllBrokerageAccounts();
        }

        public ICollection<BrokerageAccount> GetAllRetirementAccounts()
        {
            return BrokerageAccountRepository.GetAllRetirementAccounts();
        }

        public ICollection<BrokerageAccount> GetAllNonRetirementAccounts()
        {
            return BrokerageAccountRepository.GetAllNonRetirementAccounts();
        }

        public BrokerageAccount GetBrokerageAccountForActiveUser(int brokerageAccountId)
        {
            return GetBrokerageAccountForUser(brokerageAccountId, ActiveUserService.GetUserId());
        }

        public BrokerageAccount GetBrokerageAccountForUser(int brokerageAccountId, int userId)
        {
            return BrokerageAccountRepository.GetBrokerageAccountForUser(brokerageAccountId, userId);
        }

        public ICollection<BrokerageAccount> GetAllBrokerageAccountsForUser(int userId)
        {
            return BrokerageAccountRepository.GetAllBrokerageAccountsForUser(userId);
        }

        public ICollection<BrokerageAccount> GetAllRetirementAccountsForUser(int userId)
        {
            return BrokerageAccountRepository.GetAllRetirementAccountsForUser(userId);
        }

        public ICollection<BrokerageAccount> GetAllNonRetirementAccountsForUser(int userId)
        {
            return BrokerageAccountRepository.GetAllNonRetirementAccountsForUser(userId);
        }

        public List<BrokerageAccountListVm> GetBrokerageAccountListForActiveUser()
        {
            return GetBrokerageAccountList(ActiveUserService.GetUserId());
        }

        public List<BrokerageAccountListVm> GetBrokerageAccountList(int userId)
        {
            var brokerageAccounts = GetAllBrokerageAccountsForUser(userId);
            var brokerageAccountList = brokerageAccounts.Select(b =>
                                    new BrokerageAccountListVm
                                    {
                                        Id = b.Id,
                                        Title = String.Format("{0} - {1}", b.Brokerage.Title, b.Title)
                                    }).ToList();

            return brokerageAccountList;
        }

        public BrokerageAccountOverviewVm GetBrokerageAccountOverviewForActiveUser()
        {
            return GetBrokerageAccountOverviewForUser(ActiveUserService.GetUserId());
        }

        public BrokerageAccountOverviewVm GetBrokerageAccountOverviewForUser(int userId)
        {
            var nonRetirementAccounts = GetAllNonRetirementAccountsForUser(userId);
            var retirementAccounts = GetAllRetirementAccountsForUser(userId);

            return new BrokerageAccountOverviewVm
                       {
                           NonRetirementAccounts = nonRetirementAccounts,
                           RetirementAccounts = retirementAccounts
                       };
        }

        public decimal GetBrokerageAccountTotalValue(int brokerageAccountId)
        {
            throw new NotImplementedException();
        }

        public decimal GetBrokerageAccountTotalPositionValue(int brokerageAccountId)
        {
            throw new NotImplementedException();
        }

        public decimal GetBrokerageAccountTotalStockPositionValue(int brokerageAccountId)
        {
            throw new NotImplementedException();
        }

        public decimal GetBrokerageAccountTotalMutualFundValue(int brokerageAccountId)
        {
            throw new NotImplementedException();
        }

        public decimal GetBrokerageAccountTotalCashValue(int brokerageAccountId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (BrokerageAccountRepository != null)
            {
                BrokerageAccountRepository.Dispose();
            }
        }

        public bool ValidateDuplicateAccountNumber(AddBrokerageAccountVm addBrokerageAccountVm)
        {
            return ValidateDuplicateAccountNumber(addBrokerageAccountVm.AccountNumber, addBrokerageAccountVm.BrokerageId);
        }

        public bool ValidateDuplicateAccountNumber(string accountNumber, int brokerageId)
        {
            var brokerageAccount = BrokerageAccountRepository.GetBrokerageAccount(accountNumber);

            return brokerageAccount != null && brokerageAccount.BrokerageId == brokerageId;
        }

        public int CreateBrokerageAccount(AddBrokerageAccountVm addBrokerageAccountVm)
        {
            var brokerageAccount = new BrokerageAccount
            {
                Title = addBrokerageAccountVm.Title,
                IsRetirement = addBrokerageAccountVm.IsRetirement,
                AccountNumber = addBrokerageAccountVm.AccountNumber,
                UserId = ActiveUserService.GetUserId(),
                BrokerageId = addBrokerageAccountVm.BrokerageId
            };

            DbOperationStatus opStatus = BrokerageAccountRepository.InsertBrokerageAccount(brokerageAccount);
            if (opStatus.OperationSuccessStatus)
            {
                return opStatus.AffectedIndices.First();
            }
            return -1;
        }

        public bool UpdateBrokerageAccount(EditBrokerageAccountVm editBrokerageAccountVm, BrokerageAccount brokerageAccount)
        {
            if (editBrokerageAccountVm.UserId == brokerageAccount.UserId && editBrokerageAccountVm.Id == brokerageAccount.Id)
            {
                brokerageAccount.AccountNumber = editBrokerageAccountVm.AccountNumber;
                brokerageAccount.BrokerageId = editBrokerageAccountVm.BrokerageId;
                brokerageAccount.IsRetirement = editBrokerageAccountVm.IsRetirement;

                return BrokerageAccountRepository.UpdateBrokerageAccount(brokerageAccount).OperationSuccessStatus;
            }

            return false;
        }

        public int CreateBrokerage(AddBrokerageVm addBrokerageVm)
        {
            var brokerage = new Brokerage
            {
                Title = addBrokerageVm.Title
            };

            DbOperationStatus opStatus = BrokerageAccountRepository.InsertBrokerage(brokerage);
            if (opStatus.OperationSuccessStatus)
            {
                return opStatus.AffectedIndices.First();
            }
            return -1;
        }

        public bool ValidateDuplicateBrokerageTitle(string brokerageTitle)
        {
            var brokerageAccount = BrokerageAccountRepository.GetBrokerageAccount(brokerageTitle);

            return brokerageAccount != null;
        }
    }
}