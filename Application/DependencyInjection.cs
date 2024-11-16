using Application.Services;
using Application.Services.IServices;
using Application.UseCase;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidateCredentials, ValidateCredentials>();
            services.AddScoped<IValidateCredentialsService, ValidateCredentialsService>();

            services.AddScoped<IValidateRefreshToken, ValidateRefreshToken>();
            services.AddScoped<IValidateRefreshTokenService, ValidateRefreshTokenService>();

            services.AddScoped<IRevokeJwtToken, RevokeJwtToken>();
            services.AddScoped<IRevokeJwtTokenService, RevokeJwtTokenService>();

            return services;
        }
    }
}
