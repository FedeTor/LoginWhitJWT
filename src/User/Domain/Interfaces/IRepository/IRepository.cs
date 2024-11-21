using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.IRepository
{
    public interface IRepository
    {
        Task AddAsync(User entity);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> FindAllAsync(Expression<Func<User, bool>> predicate);
        Task<User> FindFirstOrDefaultAsync(Expression<Func<User, bool>> predicate);
        //Task DeleteAsync(int id);
        Task UpdateByIdAsync(int id, User entity);
    }
}
