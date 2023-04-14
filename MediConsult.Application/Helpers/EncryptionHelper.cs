using MediConsult.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MediConsult.Application.Helpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public string Encrypt(string plainText)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        public bool VerifyHash(string source, string target)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                return comparer.Compare(source, target) == 0;
            }
        }
    }
}
