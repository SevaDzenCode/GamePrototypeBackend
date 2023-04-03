using GamePrototypeBackend.BL.Services.Interfaces;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamePrototypeBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPanelController : ControllerBase
    {
        private readonly IAdminPanelService _service;
        public AdminPanelController(IAdminPanelService service)
        {
            _service = service;
        }

        [HttpPut]
        [Route("changeSettings")]
        public async Task<IActionResult> ChangeSettingsAsync(SettingsModel settings)
        {
            try
            {
                await _service.ChangeSettingsAsync(settings);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("changeExchanger")]
        public async Task<IActionResult> ChangeExchangerAsync(ExchangerModel exchangerModel)
        {
            try
            {
                await _service.ChangeExchangerAsync(exchangerModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
