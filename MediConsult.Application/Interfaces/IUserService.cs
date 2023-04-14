using MediConsult.Application.UsesCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediConsult.Application.UsesCases.Users.Request;

namespace MediConsult.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServicesResponse> CreateUser(CreateUserRequest user);

    }
}
