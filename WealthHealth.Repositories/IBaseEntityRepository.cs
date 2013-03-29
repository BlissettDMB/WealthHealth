using System;

namespace WealthHealth.Repositories
{
    public interface IBaseEntityRepository<T> : IDisposable where T : class 
    {
        T Find(int id);
        DbOperationStatus ExecuteStoredProcedure(string command);
    }
}