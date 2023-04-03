using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Models;

namespace GamePrototypeBackend.Data.Repository.Interfaces
{
    public interface IAdminPanelRepository
    {
        Task ChangeSettingsAsync(SettingsModel settings);
        Task ChangeExchangerAsync(ExchangerModel exchanger);
    }
}
