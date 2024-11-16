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
        public DbSet<TokenHistory> TokenHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedDate)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedDate)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<TokenHistory>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TokenHistory>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TokenHistory>()
                .Property(t => t.TokenCreatedDate)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<TokenHistory>()
                .Property(t => t.TokenExpiratedDate)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<TokenHistory>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
