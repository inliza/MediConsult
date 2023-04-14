using MediConsult.Application.Interfaces;
using MediConsult.Application.UsesCases.Patients;
using MediConsult.Application.UsesCases.Patients.Constants;
using MediConsult.Application.UsesCases.Users.Constants;
using MediConsult.Application.UsesCases.Users.Request;
using MediConsult.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediConsult.Application.UsesCases.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionHelper _encryption;
        private readonly IDataHelper _dataHelper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IEncryptionHelper encryption, IDataHelper dataHelper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _encryption = encryption;
            _dataHelper = dataHelper;
            _logger = logger;
        }
        public async Task<ServicesResponse> CreateUser(CreateUserRequest user)
        {
            try
            {

                var userRecord = await _userRepository.GetByProperty(UsersConstans.UserTableName, "UserName", user.UserName);
                if (userRecord == null)
                {
                    return new ServicesResponse(400, "Invalid username or password", user);
                }

                string hash = _encryption.Encrypt(user.Password);
                await _userRepository.Insert(UsersConstans.CreateUsertSP, _dataHelper.GetSqlParameters(new
                {
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    Password = hash
                }));
                return new ServicesResponse(200, "User created successfully", user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while inserting the user {user.UserName} Ex: {ex.Message}");
                return new ServicesResponse(500, $"An error occurred while inserting the user {user.UserName} Ex: {ex.Message}", user);
            }
        }
    }
}
