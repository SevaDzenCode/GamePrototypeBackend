using GamePrototypeBackend.BL.Models;
using GamePrototypeBackend.BL.Services.Interfaces;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace GamePrototypeBackend.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task Registration(RegisterModel model)
        {
            try
            {
                var user = await _userRepository.Registration(model);

                if(user == null)
                {
                    throw new Exception("User not registrated. Please, check your personal data.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GenerateEmailConfirmationToken(string email)
        {
            var token = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(token);
            }

            var tokenString = Convert.ToBase64String(token);

            await _userRepository.SaveGenerateEmailConfirmationToken(email, tokenString);

            return tokenString;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            return user;
        }

        public async Task<bool> ConfirmEmail(string email, string confirmationToken)
        {
            var token = await _userRepository.ReturnToken(email);
            
            if(token == confirmationToken)
            {
                await _userRepository.ConfirmedToken(email);
                return true;
            }

            return false;
        }
    }
}
