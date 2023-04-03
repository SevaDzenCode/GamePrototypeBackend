using GamePrototypeBackend.BL.Models;
using GamePrototypeBackend.Data.EF;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Interfaces;
using GamePrototypeBackend.Data.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GamePrototypeBackend.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GamePrototypeDbContext _context;

        public UserRepository(GamePrototypeDbContext context)
        {
            _context = context;
        }

        public async Task<User> Registration(RegisterModel model)
        {
            try
            {
                var newUser = new User()
                {
                    Nickname = model.Nickname,
                    Password = model.Password,
                    Email = model.Email,
                    RoleId = 1
                };

                await _context.Users.AddAsync(newUser);
                await AddBalanceAndCoinsWalletsForUser(newUser);
                await _context.SaveChangesAsync();

                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddBalanceAndCoinsWalletsForUser(User user)
        {
            try
            {
                var newBalance = new Balance()
                {
                    balance = 0,
                    User = user
                };

                var newCoinsWallet = new Coins()
                {
                    CountOfCoins = 0,
                    User = user
                };
                await _context.Balances.AddAsync(newBalance);
                await _context.Coins.AddAsync(newCoinsWallet);
                //await _context.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUser(LoginModel model)
        {
            User user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ReturnToken(string email)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<GamePrototypeDbContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GamePrototype;Trusted_Connection=True");

                using (var context = new GamePrototypeDbContext(optionsBuilder.Options))
                {
                    var user = await context.Users
                        .FirstOrDefaultAsync(u => u.Email == email);

                    if (user != null)
                    {
                        string emailConfirmationToken = user.EmailConfirmationToken;

                        return emailConfirmationToken;
                    }

                    return " ";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveGenerateEmailConfirmationToken(string email, string token = null)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.EmailConfirmationToken = token;
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmedToken(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;

            _context.SaveChanges();
        }
    }
}
