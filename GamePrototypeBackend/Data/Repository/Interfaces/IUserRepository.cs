using GamePrototypeBackend.BL.Models;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Models;

namespace GamePrototypeBackend.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Registration(RegisterModel model);
        Task<User> GetUser(LoginModel model);
        Task SaveGenerateEmailConfirmationToken(string email, string token = null);
        Task<User> GetUserByEmail(string email);
        Task ConfirmedToken(string email);
        Task<string> ReturnToken(string email);
    }
}
