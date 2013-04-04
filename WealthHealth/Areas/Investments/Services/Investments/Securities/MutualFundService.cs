using System.Collections.Generic;
using System.Linq;
using WealthHealth.Models.Investments;
using WealthHealth.Repositories;
using WealthHealth.Repositories.Investments;
using WealthHealth.ViewModels.Investments.MutualFunds;

namespace WealthHealth.Areas.Investments.Services.Investments.Securities
{
    public class MutualFundService : IMutualFundService
    {
        public readonly IMutualFundRepository MutualRepository;

        public MutualFundService(
            IMutualFundRepository mutualFundRepository
        )
        {
            MutualRepository = mutualFundRepository;
        }

        public MutualFund GetMutualFund(int id)
        {
            return MutualRepository.GetMutualFund(id);
        }

        public MutualFund GetMutualFund(string symbol)
        {
            return MutualRepository.GetMutualFund(symbol);
        }

        public ICollection<MutualFund> GetAllMutualFunds()
        {
            return MutualRepository.GetAllMutualFunds();
        }

        public int Create(AddMutualFundVm addMutualFundVm)
        {
            var mutualFund = new MutualFund()
            {
                Title = addMutualFundVm.Title,
                Symbol = addMutualFundVm.Symbol,
            };

            DbOperationStatus opStatus = MutualRepository.InsertMutualFund(mutualFund);
            if (opStatus.OperationSuccessStatus)
            {
                return opStatus.AffectedIndices.First();
            }
            return -1;
        }

        public bool Update(EditMutualFundVm editMutualFundVm, MutualFund mutualFund)
        {
            if (editMutualFundVm.Id == mutualFund.Id)
            {
                mutualFund.Symbol = editMutualFundVm.Symbol;
                mutualFund.Title = editMutualFundVm.Title;

                return MutualRepository.UpdateMutualFund(mutualFund).OperationSuccessStatus;
            }

            return false;
        }

        public bool ValidateDuplicateSymbol(string symbol)
        {
            var mutualFund = MutualRepository.GetMutualFund(symbol);

            return mutualFund != null;
        }
    }
}