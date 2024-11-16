using System.Linq.Expressions;

namespace Domain.Interfaces.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<int> ids);
    }
}
