using MediConsult.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConsult.Application.Interfaces
{
    public interface ITokenHelper
    {
        string CreateToken(User user);
    }
}
