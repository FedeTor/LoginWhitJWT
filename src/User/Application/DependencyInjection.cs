using Application.Services.IServices;
using Application.Services;
using Application.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.IUseCase;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IServiceCreateUser, ServiceCreateUser>();
            services.AddScoped<ICreateUser, CreateUser>();

            services.AddScoped<IServiceUpdateUser, ServiceUpdateUser>();
            services.AddScoped<IUpdateUser, UpdateUser>();

            services.AddScoped<IServiceGetUser, ServiceGetUser>();
            services.AddScoped<IGetUser, GetUser>();

            services.AddScoped<IServiceDeleteUser, ServiceDeleteUser>();
            services.AddScoped<IDeleteUser, DeleteUser>();

            services.AddScoped<IServiceGetAllUsers, ServiceGetAllUsers>();
            services.AddScoped<IGetAllUsers, GetAllUsers>();

            return services;
        }
    }
}
