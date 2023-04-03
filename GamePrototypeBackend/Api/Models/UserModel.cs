using Microsoft.AspNetCore.Identity;

namespace GamePrototypeBackend.Api.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public int RoleId { get; set; }

    }
}
