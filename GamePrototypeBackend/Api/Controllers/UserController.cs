using GamePrototypeBackend.Api.Models;
using GamePrototypeBackend.BL.Models;
using GamePrototypeBackend.BL.Services.Interfaces;
using GamePrototypeBackend.Data.Models;
using GamePrototypeBackend.Data.Repository;
using GamePrototypeBackend.Data.Repository.Interfaces;
using GamePrototypeBackend.Data.Repository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamePrototypeBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMessageSender _messageSender;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, IUserRepository userRepository,
            IConfiguration configuration,UserManager<User> userManager,
            IMessageSender messageSender)
        {
            _userService = userService;
            _userRepository = userRepository;
            _configuration = configuration;
            _userManager = userManager;
            _messageSender = messageSender;
        }   

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            try
            {
                await _userService.Registration(model);
                var token = _userService.GenerateEmailConfirmationToken(model.Email);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = model.Email }, Request.Scheme);
                await _messageSender.SendEmailAsync(model.Email, $"Для подтверждения почты перейдите по ссылке: {confirmationLink}");

                return Ok("Письмо для подтверждения почты отправлено.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("test")]
        public async Task<IActionResult> TestEmail(string email, string message)
        {
            await _messageSender.SendEmailAsync(email, message);
            return Ok();
        }

        [HttpPost]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string confirmationToken)
        {
            var result = _userService.ConfirmEmail(email, confirmationToken);

            if (result.IsCompletedSuccessfully)
            {
                return Ok("Email confirmed successfully");
            }
            else
            {
                return BadRequest("Email not confirmed");
            }
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(LoginModel model)
        {
            try
            {
                var user = await _userRepository.GetUser(model);

                if (user == null)
                {
                    return NotFound(); 
                } 

                await Authenticate(user);
                return Ok(GenerateJwtToken(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("gmail")]
        public async Task<IActionResult> MessageToGmail(string email, string message)
        {
            await _messageSender.SendEmailAsync(email, message);

            return Ok();
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
