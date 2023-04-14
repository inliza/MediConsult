using MediConsult.Domain.Primitives;
using MediConsult.Domain.Repositories;
using MediConsult.Infrastructure.Abstractions;
using MediConsult.Infrastructure.Models;
using MediConsult.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediConsult.Infrastructure
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInjections(configuration);
            return services;
        }

        private static IServiceCollection AddInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAppSettings, AppSettings>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}