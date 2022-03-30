using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Product_APi.Authorization
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; 
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "mysupersecret_secretkey!123";   
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
