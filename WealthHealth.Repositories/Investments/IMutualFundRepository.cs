using System.Collections.Generic;
using WealthHealth.Models.Investments;

namespace WealthHealth.Repositories.Investments
{
    public interface IMutualFundRepository : IBaseEntityRepository<MutualFund>
    {
        MutualFund GetMutualFund(int mutualFundId);
        MutualFund GetMutualFund(string symbol);

        ICollection<MutualFund> GetAllMutualFunds();

        DbOperationStatus InsertMutualFund(MutualFund mutualFund);
        DbOperationStatus UpdateMutualFund(MutualFund mutualFund);
        DbOperationStatus DeleteMutualFund(MutualFund mutualFund);
    }
}