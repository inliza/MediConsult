using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConsult.Application.Interfaces
{
    public interface IEncryptionHelper
    {
        string Encrypt(string plainText);
        bool VerifyHash(string source, string target);
    }
}
