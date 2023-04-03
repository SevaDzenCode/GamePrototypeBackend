using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Models;

namespace GamePrototypeBackend.BL.Services.Interfaces
{
    public interface IAdminPanelService
    {
        Task ChangeSettingsAsync(SettingsModel settings);
        Task ChangeExchangerAsync(ExchangerModel exchanger);
    }
}
