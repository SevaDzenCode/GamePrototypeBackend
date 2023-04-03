using Microsoft.AspNetCore.Identity;

namespace GamePrototypeBackend.Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        /// <summary>
        /// 1-User, 2-Admin
        /// </summary>
        public string role { get; set; }
    }
}