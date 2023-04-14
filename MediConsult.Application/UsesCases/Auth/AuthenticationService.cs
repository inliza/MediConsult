using MediConsult.Application.Interfaces;
using MediConsult.Application.UsesCases.Auth.Request;
using MediConsult.Application.UsesCases.Users;
using MediConsult.Application.UsesCases.Users.Constants;
using MediConsult.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediConsult.Application.UsesCases.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionHelper _encryption;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILogger<UserService> _logger;
        public AuthenticationService(IUserRepository userRepository, IEncryptionHelper encryption, ITokenHelper tokenHelper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _encryption = encryption;
            _tokenHelper = tokenHelper;
            _logger = logger;
        }

        public async Task<ServicesResponse> Login(LoginRequest user)
        {
            try
            {
                var userRecord = await _userRepository.GetByProperty(UsersConstans.UserTableName, "UserName", user.Username);
                if (userRecord == null)
                {
                    return new ServicesResponse(400, "Invalid username or password", user);
                }

                string hash = _encryption.Encrypt(user.Password);
                if (!_encryption.VerifyHash(hash, userRecord.Password))
                {
                    return new ServicesResponse(400, "Invalid username or password", user);
                }

                string token = _tokenHelper.CreateToken(userRecord);
                return new ServicesResponse(200, "Welcome", token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while loggin the user {user.Username} Ex: {ex.Message}");
                return new ServicesResponse(500, $"An error occurred while loggin the user {user.Username} Ex: {ex.Message}", user);
            }

        }
    }
}
