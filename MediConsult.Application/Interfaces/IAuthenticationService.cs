using MediConsult.Application.UsesCases;
using MediConsult.Application.UsesCases.Auth.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConsult.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServicesResponse> Login(LoginRequest user);
    }
}
