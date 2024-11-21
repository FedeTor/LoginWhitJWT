using Domain.Entities;
using Domain.Interfaces.IRepository;
using Infrastructure.PostgreContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.RepositoryPostgre
{
    public class Repository : IRepository
    {
        private readonly PostgreDbContext _context;
        private readonly DbSet<User> _dbSet;
        private readonly ILogger<Repository> _logger;

        public Repository(PostgreDbContext context, ILogger<Repository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<User>();
            _logger = logger;
        }

        public async Task AddAsync(User entity)
        {
            _logger.LogInformation("Start AddAsync");
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            _logger.LogInformation("Start GetByIdAsync");
            return await _context.User.FindAsync(id);
        }

        public async Task<IEnumerable<User>> FindAllAsync(Expression<Func<User, bool>> predicate)
        {
            _logger.LogInformation("Start FindAllAsync");
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<User> FindFirstOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            _logger.LogInformation("Start FindFirstOrDefaultAsync");
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task UpdateByIdAsync(int id, User entity)
        {
            _logger.LogInformation("Start UpdateByIdAsync");
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
            await _context.SaveChangesAsync();
        }
    }
}
