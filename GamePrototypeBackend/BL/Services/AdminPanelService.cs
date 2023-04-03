using GamePrototypeBackend.BL.Services.Interfaces;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Interfaces;
using GamePrototypeBackend.Data.Repository.Models;

namespace GamePrototypeBackend.BL.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly IAdminPanelRepository _repo;
        public AdminPanelService(IAdminPanelRepository repo)
        {
            _repo = repo;
        }

        public async Task ChangeSettingsAsync(SettingsModel settings)
        {
            await _repo.ChangeSettingsAsync(settings);
        }

        public async Task ChangeExchangerAsync(ExchangerModel exchanger)
        {
            await _repo.ChangeExchangerAsync(exchanger);
        }
    }
}
