using Domain.Interfaces.IRepository;
using Infrastructure.PostgreContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.RepositoryPostgre
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PostgreDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(PostgreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<int> ids)
        {
            var entitiesToDelete = await _context.TokenHistory.Where(e => ids.Contains(e.Id)).ToListAsync();

            _context.TokenHistory.RemoveRange(entitiesToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
