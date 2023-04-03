using GamePrototypeBackend.Data.EF;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Interfaces;
using GamePrototypeBackend.Data.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace GamePrototypeBackend.Data.Repository
{
    public class AdminPanelRepository : IAdminPanelRepository
    {
        private readonly GamePrototypeDbContext _context;

        public AdminPanelRepository(GamePrototypeDbContext context)
        {
            _context = context;
        }

        public async Task ChangeSettingsAsync(SettingsModel settings)
        {
            var newSettings = new Settings
            {
                MinWithdrawal = settings.MinWithdrawal,
                MinDeposit = settings.MinDeposit,
                SumFromRefferals = settings.SumFromRefferals,
            };
            await _context.Settings.AddAsync(newSettings);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeExchangerAsync(ExchangerModel exchanger)
        {
            var newExchanger = new Exchanger
            {
                CountOfCoins = exchanger.CountOfCoins,
                CoinsCost = exchanger.CoinsCost,
                ActualData = DateTime.Now
            };
            await _context.Exchangers.AddAsync(newExchanger);
            await _context.SaveChangesAsync();
        }
    }
}
