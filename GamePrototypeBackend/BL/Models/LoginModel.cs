using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GamePrototypeBackend.BL.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter your email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
