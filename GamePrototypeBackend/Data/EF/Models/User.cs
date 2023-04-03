using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePrototypeBackend.Data.Models
{
    public class User : IdentityUser<int>
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string? EmailConfirmationToken { get; set; }
        public Role? Role { get; set; }

        [NotMapped]
        public List<Refferal>? refferals { get; set; }
    }
}
