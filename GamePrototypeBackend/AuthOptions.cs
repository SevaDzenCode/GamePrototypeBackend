using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GamePrototypeBackend
{
    public class AuthOptions
    {
        public const string ISSUER = "GamePrototype";
        public const string AUDIENCE = "GamePrototypeClient";
        const string KEY = "298c5700-ecb3-4f00-8423-0b9aebbcd316";
        public static readonly TimeSpan LifeTime = TimeSpan.FromDays(2);
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
