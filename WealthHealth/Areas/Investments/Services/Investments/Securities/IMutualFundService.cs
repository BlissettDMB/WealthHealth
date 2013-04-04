using System.Collections.Generic;
using WealthHealth.Models.Investments;
using WealthHealth.ViewModels.Investments.MutualFunds;

namespace WealthHealth.Areas.Investments.Services.Investments.Securities
{
    public interface IMutualFundService
    {
        MutualFund GetMutualFund(int id);
        MutualFund GetMutualFund(string symbol);

        ICollection<MutualFund> GetAllMutualFunds();

        int Create(AddMutualFundVm addMutualFundVm);
        bool Update(EditMutualFundVm editMutualFundVm, MutualFund mutualFund);

        bool ValidateDuplicateSymbol(string symbol);
    }
}