using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.PostgreContext
{
    public class PostgreDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<User> User { get; set; }
    }
}
