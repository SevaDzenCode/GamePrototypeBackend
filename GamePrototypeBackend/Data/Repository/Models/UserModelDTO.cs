using Microsoft.AspNetCore.Identity;

namespace GamePrototypeBackend.Data.Repository.Models
{
    public class UserModelDTO : IdentityUser
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
