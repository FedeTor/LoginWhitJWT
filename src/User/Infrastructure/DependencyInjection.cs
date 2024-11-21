using Domain.Entities;
using Domain.Interfaces.IRepository;
using Infrastructure.PostgreContext;
using Infrastructure.RepositoryPostgre;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PostgreDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
