using FluentValidation;
using MediConsult.Application.Helpers;
using MediConsult.Application.Interfaces;
using MediConsult.Application.UsesCases.Auth;
using MediConsult.Application.UsesCases.Patients;
using MediConsult.Application.UsesCases.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediConsult.Application
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                    .AddServices();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEncryptionHelper, EncryptionHelper>();
            services.AddTransient<IDataHelper, DataHelper>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenHelper, TokenHelper>();
            return services;
        }
    }
}