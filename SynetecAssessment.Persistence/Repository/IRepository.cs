using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Repository
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> DbSet { get; }
        Task<T> GetAsync(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
