using System;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func);
    }
}
