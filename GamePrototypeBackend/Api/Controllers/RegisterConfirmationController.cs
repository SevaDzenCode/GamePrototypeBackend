using GamePrototypeBackend.BL.Services.Interfaces;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace GamePrototypeBackend.Api.Controllers
{
    [AllowAnonymous]
    public class RegisterConfirmationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageSender _messageSender;
        private readonly IUserRepository _userRepository;

        public RegisterConfirmationController(UserManager<User> userManager, IMessageSender messageSender)
        {
            _userManager = userManager;
            _messageSender = messageSender;
        }

        public string Email { get; set; }
        public bool DisplayConfirmAccountLink { get; set; }
        public string EmailConfirmationUrl { get; set; }

        [HttpPost]
        [Route("he")]
        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
                return NotFound("Please, enter your email");

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("User not found");

            Email = email;

            DisplayConfirmAccountLink = false;

            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }
            return Ok();
        }
    }
}
