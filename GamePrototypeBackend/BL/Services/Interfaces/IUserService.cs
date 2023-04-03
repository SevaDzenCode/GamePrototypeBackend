using GamePrototypeBackend.BL.Models;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Models;

namespace GamePrototypeBackend.BL.Services.Interfaces
{
    public interface IUserService
    {
        //Task AddUser(User userModel);
        Task Registration(RegisterModel model);
        Task<string> GenerateEmailConfirmationToken(string email);
        Task<User> GetUserByEmail(string email);
        Task<bool> ConfirmEmail(string email, string confirmationToken);
    }
}
